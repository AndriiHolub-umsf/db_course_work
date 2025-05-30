using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using EsportApp.Models;
using Microsoft.AspNetCore.Authorization;


namespace EsportApp.Controllers
{
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly string _connectionString;

        public GameController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetGames()
        {
            using var conn = new MySqlConnection(_connectionString);
            var sql = @"SELECT id, gname, photo, description FROM game;";
            var games = (await conn.QueryAsync<GameDto>(sql)).AsList();
            foreach (var g in games)
                if (g.Photo != null && g.Photo.Length > 0)
                    g.PhotoBase64 = Convert.ToBase64String(g.Photo);
            return Ok(games);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddGame([FromForm] GameCreateDto dto)
        {
            using var conn = new MySqlConnection(_connectionString);

            byte[] photoBytes = null;

            if (dto.Photo != null && dto.Photo.Length > 0)
            {
                string Err = Utils.ValidateImage(dto.Photo);
                if (Err.Length > 0)
                    return BadRequest(new { message = Err });

                photoBytes = await Utils.FileToBytes(dto.Photo);
            }


            string sql = "INSERT INTO game (gname, photo, description) VALUES (@Name, @Photo, @Description);";
            await conn.ExecuteAsync(sql, new
            {
                Name = dto.Name,
                Photo = photoBytes,
                Description = dto.Description
            });
            return Ok(new { message = "Гру добавлено!" });
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteGame([FromBody] IdDto dto)
        {
            using var conn = new MySqlConnection(_connectionString);
            string sql = "DELETE FROM game WHERE id = @Id;";
            await conn.ExecuteAsync(sql, new { Id = dto.Id });
            return Ok(new { message = "Гру видаленно!" });
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("edit")]
        public async Task<IActionResult> EditGame([FromForm] GameEditDto dto)
        {
            using var conn = new MySqlConnection(_connectionString);
            byte[] photoBytes = null;
            string photoSql = "";

            if (dto.Photo != null && dto.Photo.Length > 0)
            {
                string Err = Utils.ValidateImage(dto.Photo);
                if (Err.Length > 0)
                    return BadRequest(new { message = Err });

                photoBytes = await Utils.FileToBytes(dto.Photo);
                photoSql = ", photo=@Photo";
            }

            string sql = $"UPDATE game SET gname=@Name, description=@Description{photoSql} WHERE id=@Id;";
            await conn.ExecuteAsync(sql, new
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Photo = photoBytes
            });
            return Ok(new { message = "Гру зміненно!" });
        }


        [HttpPost("teams")]
        public async Task<IActionResult> GetGameTeams([FromBody] GameTeamsRequest req)
        {
            using var conn = new MySqlConnection(_connectionString);
            var sql = @"SELECT t.tname AS Team_Name, p.pname AS Captain_Name, t.photo AS Team_Photo
                        FROM team t
                        JOIN player p ON t.captain_id = p.pid
                        JOIN game_team g ON t.tid = g.tid
                        WHERE g.gname = @Gname;";
            var teams = (await conn.QueryAsync<GameTeamDto>(sql, new { Gname = req.Id })).AsList();
            foreach (var t in teams)
                if (t.Team_Photo != null && t.Team_Photo.Length > 0)
                    t.Team_PhotoBase64 = Convert.ToBase64String(t.Team_Photo);
            return Ok(teams);
        }
    }
}
