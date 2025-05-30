using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using EsportApp.Models;
using EsportApp;
using Microsoft.AspNetCore.Authorization;

namespace EsportApp.Models
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly string _connectionString;

        public TeamController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetTeams()
        {
            using var conn = new MySqlConnection(_connectionString);
            var sql = @"SELECT t.tname,t.id, p.pname AS captain_name, t.photo
                        FROM team t
                        LEFT JOIN player p ON t.captain_id = p.pid;";
            var result = (await conn.QueryAsync<TeamDetailsDto>(sql)).AsList();

            foreach (var t in result)
            {
                t.PhotoBase64 = Utils.getTeamLogoBase64Photo(t.Photo);
            }
            return Ok(result);
        }


        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetTeamDetails(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
   
            var sqlTeam = @"SELECT id, tname, captain_id, photo FROM team WHERE id=@Id";
            var team = await conn.QueryFirstOrDefaultAsync<TeamDetailsDto>(sqlTeam, new { Id = id });
            if (team == null) 
                return NotFound();


            var sqlPlayers = @"SELECT p.pid, p.pname, p.photo
                       FROM player p
                       JOIN player_team pt ON pt.pid = p.pid
                       WHERE pt.tid = @Id";
            var players = (await conn.QueryAsync<PlayerShortDto>(sqlPlayers, new { Id = id })).ToList();
            foreach (var p in players)
                p.PhotoBase64 = Utils.getAvatarBase64Photo(p.Photo);

            var sqlTournaments = @"SELECT t.id, t.name, t.date
                           FROM tournament t
                           JOIN tournament_team tt ON t.id = tt.tournament_id
                           WHERE tt.team_id = @Id";
            var tournaments = (await conn.QueryAsync<TournamentShortDto>(sqlTournaments, new { Id = id })).ToList();

            return Ok(new
            {
                team.Id,
                tname = team.Tname,
                capitan_id = team.Captain_id,
                photoBase64 = Utils.getTeamLogoBase64Photo(team.Photo),
                players,
                tournaments
            });
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddTeam([FromForm] TeamCreateDto dto)
        {
      
            var email = User?.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "Потрібна авторизація" });

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            using var transaction = await conn.BeginTransactionAsync();

            try
            {

                var playerId = await conn.ExecuteScalarAsync<int>(
                    "SELECT player_id FROM login WHERE email = @Email", new { Email = email }, transaction);
                if (playerId == 0)
                    throw new Exception("Немає пов'язаних даних гравця");

    
                string sql = "INSERT INTO team (tname, photo, captain_id) VALUES (@Name, @Photo, @CaptainId); SELECT LAST_INSERT_ID();";
                var tid = await conn.ExecuteScalarAsync<long>(sql, new
                {
                    Name = dto.Name,
                    Photo = dto.Photo != null && dto.Photo.Length > 0 ? await Utils.FileToBytes(dto.Photo) : null,
                    CaptainId = playerId
                }, transaction);

 
                await conn.ExecuteAsync("INSERT INTO player_team (tid, pid) VALUES (@Tid, @Pid)",
                    new { Tid = tid, Pid = playerId }, transaction);

                await transaction.CommitAsync();
                return Ok(new { message = "Команда успішно створена!" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new { message = "Помилка створення команди", detail = ex.Message });
            }
        }



        [Authorize]
        [HttpGet("my-captain")]
        public async Task<IActionResult> GetMyCaptainTeams()
        {
            var email = User?.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            using var conn = new MySqlConnection(_connectionString);


            var playerId = await conn.QueryFirstOrDefaultAsync<int?>(
                "SELECT player_id FROM LOGIN WHERE email = @Email", new { Email = email });
            if (playerId == null)
                return NotFound(new { message = "Player not found" });

            var teams = (await conn.QueryAsync<MyTeamShortDto>(
                "SELECT id, tname FROM team WHERE captain_id = @Pid",
                new { Pid = playerId.Value })).ToList();

            return Ok(teams);
        }

        [Authorize]
        [HttpGet("{teamId}/is-captain")]
        public async Task<IActionResult> IsCaptain(int teamId)
        {
            var email = User.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            using var conn = new MySqlConnection(_connectionString);

            var playerId = await conn.QueryFirstOrDefaultAsync<int?>(
                "SELECT player_id FROM login WHERE email = @Email", new { Email = email });

            if (playerId == null)
                return NotFound();

            var isCaptain = await conn.QueryFirstOrDefaultAsync<bool>(
                "SELECT CASE WHEN captain_id = @PlayerId THEN 1 ELSE 0 END FROM team WHERE id = @TeamId",
                new { PlayerId = playerId, TeamId = teamId });

            return Ok(new { isCaptain });
        }

        [Authorize]
        [HttpPost("{teamId}/add_player")]
        public async Task<IActionResult> AddPlayerByEmail(int teamId, [FromBody] AddPlayerByEmailDto dto)
        {
            var email = User?.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "Потрібна авторизація" });

            using var conn = new MySqlConnection(_connectionString);

            var isCaptain = await conn.ExecuteScalarAsync<bool>(
                "SELECT COUNT(*) FROM team WHERE id = @Tid AND captain_id = (SELECT player_id FROM login WHERE email = @Email)",
                new { Tid = teamId, Email = email });

            if (!isCaptain)
                return Forbid();

            var pid = await conn.ExecuteScalarAsync<int?>(
                "SELECT player_id FROM login WHERE email = @TargetEmail", new { TargetEmail = dto.Email });

            if (pid == null)
                return NotFound(new { message = "Гравець із таким email не знайдений" });

   
            var exists = await conn.ExecuteScalarAsync<bool>(
                "SELECT COUNT(*) FROM player_team WHERE tid = @Tid AND pid = @Pid", new { Tid = teamId, Pid = pid.Value });

            if (exists)
                return BadRequest(new { message = "Гравець уже в команді" });

            await conn.ExecuteAsync(
                "INSERT INTO player_team (tid, pid) VALUES (@Tid, @Pid)", new { Tid = teamId, Pid = pid.Value });

            return Ok(new { message = "Гравець додано до команди!" });
        }

        [Authorize]
        [HttpPost("{teamId}/remove_player")]
        public async Task<IActionResult> RemovePlayer(int teamId, [FromBody] RemovePlayerRequest req)
        {
            var email = User?.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "Потрібна авторизація" });

            using var conn = new MySqlConnection(_connectionString);

            var isCaptain = await conn.ExecuteScalarAsync<bool>(
                "SELECT COUNT(*) FROM team t JOIN login l ON t.captain_id = l.player_id WHERE t.id = @Tid AND l.email = @Email",
                new { Tid = teamId, Email = email }
            );

            if (!isCaptain)
                return Forbid();

            var sql = "DELETE FROM player_team WHERE tid = @TeamId AND pid = @PlayerId";
            var rows = await conn.ExecuteAsync(sql, new { TeamId = teamId, PlayerId = req.PlayerId });

            if (rows > 0)
                return Ok(new { message = "Гравеця вилучено з команди" });
            return BadRequest(new { message = "Помилка вилучення гравця" });
        }
    }
}