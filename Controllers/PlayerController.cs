using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using EsportApp.Models;
using System.Numerics;
namespace EsportApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly string _connectionString;

        public PlayerController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetPlayerDetails(int id)
        {
            using var conn = new MySqlConnection(_connectionString);

        
            var sql = @"SELECT pid, pname, sex, dob, photo FROM player WHERE pid = @Id";
            var player = await conn.QueryFirstOrDefaultAsync<PlayerDetailsDto>(sql, new { Id = id });
            if (player == null) 
                return NotFound();


            player.PhotoBase64 = Utils.getAvatarBase64Photo(player.Photo);

            
            var teams = (await conn.QueryAsync(@"
                        SELECT t.id, t.tname, t.captain_id
                            FROM team t
                            JOIN player_team pt ON pt.tid = t.id
                            WHERE pt.pid = @Id
                        ", new { Id = id })).ToList();

           
            player.Teams = teams.Select(t => new TeamShortDto
            {
                Id = t.id,
                Name = t.tname,
                Сaptain_id = t.captain_id
            }).ToList();

           
            var tournaments = (await conn.QueryAsync(@"
                               SELECT DISTINCT tr.id, tr.name, tr.date
                                    FROM tournament tr
                                    JOIN tournament_team tt ON tr.id = tt.tournament_id
                                    JOIN team t ON t.id = tt.team_id
                                    JOIN player_team pt ON pt.tid = t.id
                                    WHERE pt.pid = @Id
                                    ORDER BY tr.date DESC
                                ", new { Id = id })).ToList();

            player.Tournaments = tournaments.Select(tr => new TournamentShortDto
            {
                Id = tr.id,
                Name = tr.name,
                Date = tr.date
            }).ToList();

            return Ok(player);
        }


        [HttpGet("unassigned")]
        public async Task<IActionResult> GetUnassignedPlayers()
        {
            using var conn = new MySqlConnection(_connectionString);
            var sql = @"SELECT * FROM player WHERE pid NOT IN (SELECT pid FROM player_team);";
            var result = (await conn.QueryAsync<PlayerDto>(sql)).AsList();
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPlayer([FromForm] PlayerCreateDto dto)
        {
            using var conn = new MySqlConnection(_connectionString);
            string sql = @"INSERT INTO player (pname, dob, sex, photo) VALUES (@Name, @Dob, @Sex, @Photo);";
            var photo = dto.Photo != null ? await FileToBytes(dto.Photo) : null;
            var p = new
            {
                Name = dto.Name,
                Dob = dto.Dob,
                Sex = dto.Sex,
                Photo = photo
            };
            try
            {
                await conn.ExecuteAsync(sql, p);
                return Ok(new { message = "Гравця доданно!" });
            }
            catch
            {
                return BadRequest(new { message = "Error inserting data" });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPlayers()
        {
            using var conn = new MySqlConnection(_connectionString);
            var sql = @"SELECT p.pname, DATE_FORMAT(p.dob, '%Y-%m-%d') AS dob,p.pid, p.sex, p.photo
                FROM player p";
            var result = (await conn.QueryAsync<PlayerDetailsDto>(sql)).AsList();
            foreach (var p in result)
                p.PhotoBase64 = Utils.getAvatarBase64Photo(p.Photo);

            return Ok(result);
        }
        private async Task<byte[]> FileToBytes(Microsoft.AspNetCore.Http.IFormFile file)
        {
            using var ms = new System.IO.MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}
