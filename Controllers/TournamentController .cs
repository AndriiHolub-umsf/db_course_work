using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using EsportApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Numerics;
using System.Security.Claims;

namespace EsportApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentController : ControllerBase
    {
        private readonly string _connectionString;
        public TournamentController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<IActionResult> GetTournaments()
        {
            using var conn = new MySqlConnection(_connectionString);
            var sql = @"SELECT t.id, g.gname, t.name, t.date, t.max_teams,
                               t.points_to_win, t.players_in_team, g.photo AS game_photo
                               FROM tournament t JOIN game g ON t.gid = g.id";

            var tournaments = (await conn.QueryAsync<TournamentDetailsDto>(sql)).ToList();
            foreach(TournamentDetailsDto tournament in tournaments)
            {
                tournament.GamePhotoBase64 = Utils.getGameBase64Photo(tournament.Game_photo);
            }
      
            return Ok(tournaments);
        }

        [HttpGet("{id}/info")]
        public async Task<IActionResult> GetTournamentInfo(int id)
        {
            using var conn = new MySqlConnection(_connectionString);


            var sql = @"SELECT t.id, t.name, t.date, t.max_teams, t.points_to_win, t.players_in_team,t.winner_id AS winner_id,
                    g.gname, g.photo AS game_photo
                FROM tournament t
                JOIN game g ON t.gid = g.id
                WHERE t.id = @Id";
            var t = await conn.QueryFirstOrDefaultAsync(sql, new { Id = id });
            if (t == null) return NotFound();


            int approvedCount = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM tournament_team WHERE tournament_id=@Tid AND status='approved'", new { Tid = id });


            bool started = DateTime.UtcNow.Date >= ((DateTime)t.date).Date;


            List<TournamentRequestDto> requests = new();
            string role = null;
            string email = null;
            int? myPlayerId = null;
            if (User?.Identity?.IsAuthenticated == true)
            {
                role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                email = User.Identity.Name;
                if (!string.IsNullOrEmpty(email))
                    myPlayerId = await conn.ExecuteScalarAsync<int?>(
                        "SELECT player_id FROM login WHERE email=@Email", new { Email = email });
            }
            if (role == "Admin")
            {
                requests = (await conn.QueryAsync<TournamentRequestDto>(
                    @"SELECT t.team_id AS TeamId, tm.tname AS TeamName, t.status AS Status, 
                     p.pname AS CaptainName, l.email AS CaptainEmail
                     FROM tournament_team t
                     JOIN team tm ON t.team_id = tm.id
                     JOIN player p ON tm.captain_id = p.pid
                     JOIN login l ON l.player_id = p.pid
                     WHERE t.tournament_id = @Tid", new { Tid = id })).ToList();
            }


            var teams = (await conn.QueryAsync(
                @"SELECT tm.id, tm.tname, tm.photo FROM tournament_team t 
                    JOIN team tm ON t.team_id = tm.id
                    WHERE t.tournament_id=@Tid AND t.status='approved'", new { Tid = id }));


            var Teams = teams.Select(x => new TeamShortDto
             {
                 Id = x.id,
                 Name = x.tname,
                 PhotoBase64 = Utils.getTeamLogoBase64Photo(x.photo)
             }).ToList();

            TournamentMyRequestDto myRequest = null;
            if (myPlayerId != null)
            {

                var myTeams = (await conn.QueryAsync<int>("SELECT id FROM team WHERE captain_id=@Pid", new { Pid = myPlayerId })).ToList();
                if (myTeams.Any())
                {
                    var req = await conn.QueryFirstOrDefaultAsync(
                        "SELECT team_id, status FROM tournament_team WHERE tournament_id=@Tid AND team_id IN @Tids",
                        new { Tid = id, Tids = myTeams });
                    if (req != null)
                        myRequest = new TournamentMyRequestDto { TeamId = req.team_id, Status = req.status };
                }
            }


            var matches = (await conn.QueryAsync<MatchDto>(
                @"SELECT m.id, m.round, ta.tname AS TeamA, tb.tname AS TeamB, 
                  ta.id AS Team_a_id, tb.id AS Team_b_id,
                  m.score_a AS ScoreA, m.score_b AS ScoreB, m.winner_id AS WinnerId
                  FROM `match` m
                  JOIN team ta ON m.team_a_id = ta.id
                  JOIN team tb ON m.team_b_id = tb.id
                  WHERE m.tournament_id = @Tid
                 ORDER BY m.round, m.id", new { Tid = id })).ToList();

            string wName = null;
            if (t.winner_id != null && Teams.Count>0)
            {
                foreach(var team in Teams)
                {
                    if (team.Id == t.winner_id)
                    {
                        wName = team.Name;
                        break;
                    }
                }
            }
            return Ok(new TournamentInfoDto
            {
                Id = t.id,
                Name = t.name,
                GameName = t.gname,
                WinnerId = t.winner_id,
                WinnerName = wName,
                GamePhotoBase64 = Utils.getGameBase64Photo(t.game_photo),
                Date = t.date,
                MaxTeams = t.max_teams,
                PointsToWin = t.points_to_win,
                PlayersInTeam = t.players_in_team,
                ApprovedCount = approvedCount,
                Started = started,
                Teams = Teams,
                Requests = requests,
                MyRequest = myRequest,
                Matches = matches
            });
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentRequest req)
        {
            using var conn = new MySqlConnection(_connectionString);
            var sql = @"INSERT INTO tournament 
                    (gid, name, date, max_teams, points_to_win, players_in_team) 
                    VALUES (@Gid, @Name, @Date, @MaxTeams, @PointsToWin, @PlayersInTeam); 
                    SELECT LAST_INSERT_ID();";
            var tid = await conn.ExecuteScalarAsync<int>(sql, new
            {
                req.Gid,
                req.Name,
                req.Date,
                req.MaxTeams,
                req.PointsToWin,
                req.PlayersInTeam
            });
            return Ok(new { message = "Турнір створенно", TournamentId = tid });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTournament(int id, [FromBody] CreateTournamentRequest req)
        {
            using var conn = new MySqlConnection(_connectionString);
            var sql = @"UPDATE tournament 
                    SET gid = @Gid, name = @Name, date = @Date, 
                        max_teams = @MaxTeams, points_to_win = @PointsToWin, players_in_team = @PlayersInTeam 
                    WHERE id = @Id";
            await conn.ExecuteAsync(sql, new
            {
                Id = id,
                req.Gid,
                req.Name,
                req.Date,
                req.MaxTeams,
                req.PointsToWin,
                req.PlayersInTeam
            });
            return Ok(new { message = "Турнір змінено" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{tid}/set_winner")]
        public async Task<IActionResult> SetWinner(int tid, [FromBody] SetWinnerDto dto)
        {
            using var conn = new MySqlConnection(_connectionString);
            var exists = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM tournament_team WHERE tournament_id=@Tid AND team_id=@WinnerId AND status='approved'",
                new { Tid = tid, WinnerId = dto.TeamId });
            if (exists == 0)
                return BadRequest(new { message = "Ця команда не брала участі у турнірі!" });

            await conn.ExecuteAsync("UPDATE tournament SET winner_id=@WinnerId WHERE id=@Tid", new { WinnerId = dto.TeamId, Tid = tid });
            return Ok(new { message = "Переможця встановлено." });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.ExecuteAsync("DELETE FROM tournament_team WHERE tournament_id = @Id", new { Id = id });
            await conn.ExecuteAsync("DELETE FROM `match` WHERE tournament_id = @Id", new { Id = id });
            await conn.ExecuteAsync("DELETE FROM tournament WHERE id = @Id", new { Id = id });
            return Ok(new { message = "Турнір видаленно" });
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("{tid}/team/{teamId}/approve")]
        public async Task<IActionResult> ApproveTeam(int tid, int teamId)
        {
            using var conn = new MySqlConnection(_connectionString);

            var maxTeams = await conn.ExecuteScalarAsync<int>(
                "SELECT max_teams FROM tournament WHERE id=@Tid", new { Tid = tid });
            var approved = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM tournament_team WHERE tournament_id=@Tid AND status='approved'",
                new { Tid = tid });

            if (approved >= maxTeams)
                return BadRequest(new { message = "Ліміт команд вже заповнено" });

            await conn.ExecuteAsync(
                "UPDATE tournament_team SET status='approved' WHERE tournament_id=@Tid AND team_id=@TeamId",
                new { Tid = tid, TeamId = teamId });
            return Ok(new { message = "Заявка підтверджена" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{tid}/team/{teamId}/reject")]
        public async Task<IActionResult> RejectTeam(int tid, int teamId)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.ExecuteAsync(
                "UPDATE tournament_team SET status='rejected' WHERE tournament_id=@Tid AND team_id=@TeamId",
                new { Tid = tid, TeamId = teamId });
            return Ok(new { message = "Заявка відхилена" });
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{tid}/requests")]
        public async Task<IActionResult> GetTournamentRequests(int tid)
        {
            using var conn = new MySqlConnection(_connectionString);

            var sql = @"SELECT tt.id, t.tname, tt.team_id, tt.status, 
                    p.pname AS captain_name, l.email AS captain_email
                FROM tournament_team tt
                JOIN team t ON tt.team_id = t.id
                JOIN player p ON t.captain_id = p.pid
                JOIN login l ON l.player_id = p.pid
                WHERE tt.tournament_id = @Tid
                ORDER BY tt.status, t.tname";

            var requests = (await conn.QueryAsync(sql, new { Tid = tid }))
                .Select(r => new
                {
                    Id = r.id,
                    TeamId = r.team_id,
                    TeamName = r.tname,
                    Status = r.status,
                    CaptainName = r.captain_name,
                    CaptainEmail = r.captain_email
                }).ToList();


            var maxTeams = await conn.ExecuteScalarAsync<int>(
                "SELECT max_teams FROM tournament WHERE id=@Tid", new { Tid = tid });
            var currentApproved = requests.Count(r => r.Status == "approved");

            return Ok(new
            {
                MaxTeams = maxTeams,
                ApprovedCount = currentApproved,
                Requests = requests
            });
        }

        [Authorize]
        [HttpPost("{tid}/submit_an_app")]
        public async Task<IActionResult> SubmitToTournament(int tid, [FromBody] TeamAppRequest req)
        {
            var email = User?.Identity?.Name;
            using var conn = new MySqlConnection(_connectionString);

 
            var playerId = await conn.ExecuteScalarAsync<int?>(
                "SELECT player_id FROM login WHERE email = @Email", new { Email = email });
            if (playerId == null) return Unauthorized();


            var isCaptain = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM team WHERE id = @TeamId AND captain_id = @Pid",
                new { TeamId = req.TeamId, Pid = playerId });
            if (isCaptain == 0)
                return BadRequest(new { message = "Ви не капітан обраної команди" });


            var exists = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM tournament_team WHERE tournament_id=@Tid AND team_id=@TeamId",
                new { Tid = tid, TeamId = req.TeamId });
            if (exists > 0)
                return BadRequest(new { message = "Ви вже подали заявку цією командою" });

            var maxTeams = await conn.ExecuteScalarAsync<int>(
                "SELECT max_teams FROM tournament WHERE id=@Tid", new { Tid = tid });
            var currentApproved = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM tournament_team WHERE tournament_id=@Tid AND status='approved'",
                new { Tid = tid });
            if (currentApproved >= maxTeams)
                return BadRequest(new { message = "Досягнуто максимальної кількості команд" });


            await conn.ExecuteAsync(
                "INSERT INTO tournament_team (tournament_id, team_id, status) VALUES (@Tid, @TeamId, 'pending')",
                new { Tid = tid, TeamId = req.TeamId });

            return Ok(new { message = "Заявка подана. Чекайте на підтвердження." });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{tid}/match/create")]
        public async Task<IActionResult> CreateMatch(int tid, [FromBody] CreateMatchDto dto)
        {
            if (dto.TeamAId == dto.TeamBId)
                return BadRequest(new { message = "Не можна створювати матч однієї команди!" });

            using var conn = new MySqlConnection(_connectionString);

            var checkSql = @"SELECT COUNT(*) FROM tournament_team 
                 WHERE tournament_id=@Tid AND team_id IN (@A, @B) AND status='approved'";
            var count = await conn.ExecuteScalarAsync<int>(checkSql, new { Tid = tid, A = dto.TeamAId, B = dto.TeamBId });
            if (count != 2)
                return BadRequest(new { message = "Обидві команди мають бути схвалені у турнірі." });

            var exist = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM `match` WHERE tournament_id=@Tid AND round=@Round AND team_a_id=@A AND team_b_id=@B",
                new { Tid = tid, Round = dto.Round, A = dto.TeamAId, B = dto.TeamBId }
            );
            if (exist > 0)
                return BadRequest(new { message = "Такий матч вже існує" });

            await conn.ExecuteAsync(
                @"INSERT INTO `match` (tournament_id, round, team_a_id, team_b_id, score_a, score_b)
                 VALUES (@Tid, @Round, @A, @B, 0, 0)",
                new { Tid = tid, Round = dto.Round, A = dto.TeamAId, B = dto.TeamBId }
            );

            return Ok(new { message = "Матч створен." });
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{tid}/match/{mid}/score")]
        public async Task<IActionResult> UpdateScore(int tid, int mid, [FromBody] UpdateMatchResultRequest dto)
        {
     
            if (dto.Team > 1 || dto.Team < 0)
                return BadRequest();
            if (dto.Change != 1 && dto.Change != -1)
                return BadRequest();

            using var conn = new MySqlConnection(_connectionString);
            var field = dto.Team == 0 ? "score_a" : "score_b";
            var sql = $@"UPDATE `match` SET {field} = GREATEST(0, {field} + @Change)
                WHERE id=@Mid AND tournament_id=@Tid";
            await conn.ExecuteAsync(sql, new { Tid = tid, Mid = mid, Change = dto.Change });
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{tid}/match/{mid}/finish")]
        public async Task<IActionResult> FinishMatch(int tid, int mid)
        {
            using var conn = new MySqlConnection(_connectionString);

            var match = await conn.QueryFirstOrDefaultAsync<dynamic>(
                "SELECT score_a, score_b, team_a_id, team_b_id FROM `match` WHERE id=@Mid AND tournament_id=@Tid",
                new { Mid = mid, Tid = tid });

            if (match == null) return NotFound();

            int? winner = null;
            if (match.score_a > match.score_b) winner = match.team_a_id;
            if (match.score_b > match.score_a) winner = match.team_b_id;

            await conn.ExecuteAsync("UPDATE `match` SET winner_id=@Winner WHERE id=@Mid", new { Winner = winner, Mid = mid });
            return Ok(new { winner });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{tid}/match/{mid}")]
        public async Task<IActionResult> DeleteMatch(int tid, int mid)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.ExecuteAsync("DELETE FROM `match` WHERE id=@Mid AND tournament_id=@Tid", new { Mid = mid, Tid = tid });
            return Ok();
        }


    }
}
