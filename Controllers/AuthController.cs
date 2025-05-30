using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using EsportApp.Models;
using Microsoft.AspNetCore.Identity.Data;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp;

namespace EsportApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly PasswordHasher<string> _hasher = new();
        private readonly string JwtKey;

        public AuthController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
            JwtKey = config.GetSection("AuthOptions")["KEY"];
        }

       
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto req)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            using var tx = await conn.BeginTransactionAsync();

            try
            {

                string sql = "SELECT email FROM LOGIN WHERE email = @Email;";
                var user = await conn.QueryFirstOrDefaultAsync<UserInfo>(sql, new { Email = req.Email }, tx);
                if (user != null)
                    return BadRequest(new { message = "Користувач вже існує" });


                sql = @"INSERT INTO player (pname, dob, sex) VALUES (@Name, @Dob, @Sex);
                SELECT LAST_INSERT_ID();";
                int playerId = await conn.ExecuteScalarAsync<int>(sql, new
                {
                    Name = req.Name,
                    Dob = req.Dob,
                    Sex = req.Sex
                }, tx);


                var hash = _hasher.HashPassword(req.Email, req.Password);
                sql = @"INSERT INTO LOGIN (email, pwd, role, player_id) VALUES (@Email, @Pwd, 'User', @PlayerId);";
                await conn.ExecuteAsync(sql, new { Email = req.Email, Pwd = hash, PlayerId = playerId }, tx);

                await tx.CommitAsync();
                return Ok(new { message = "Реєстрація пройшла успішно!" });
            }
            catch
            {
                await tx.RollbackAsync();
                return BadRequest(new { message = "Помилка реєстрації" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto req)
        {
            using var conn = new MySqlConnection(_connectionString);
            string sql = "SELECT email, pwd, role FROM LOGIN WHERE email = @Email;";
            var user = await conn.QueryFirstOrDefaultAsync<UserInfo>(sql, new { Email = req.Email });
            if (user == null) return Unauthorized(new { message = "Недійсні облікові дані" });

            var result = _hasher.VerifyHashedPassword(req.Email, user.Pwd, req.Password);

            if(result == PasswordVerificationResult.SuccessRehashNeeded)
            {

                var newHash = _hasher.HashPassword(req.Email, req.Password);
                string updateSql = "UPDATE LOGIN SET pwd = @Pwd WHERE email = @Email;";
                await conn.ExecuteAsync(updateSql, new { Pwd = newHash, Email = req.Email });

                result = PasswordVerificationResult.Success;
            }

            if (result == PasswordVerificationResult.Success)
            {
                string token = CreateToken(user.Email, user.Role);

                return Ok(token);
            }
            else
                return Unauthorized(new { message = "Недійсні облікові дані" });
        }

        [Authorize]
        [HttpPost("changepsw")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto req)
        {

            var email = User?.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "Потрібна авторизація" });

            using var conn = new MySqlConnection(_connectionString);
     
            string sql = "SELECT email, pwd FROM LOGIN WHERE email = @Email;";
            var user = await conn.QueryFirstOrDefaultAsync<UserInfo>(sql, new { Email = email });
            if (user == null)
                return Unauthorized(new { message = "Пользователь не найден" });

            var result = _hasher.VerifyHashedPassword(email, user.Pwd, req.OldPassword);
            if (result != PasswordVerificationResult.Success)
                return BadRequest(new { message = "Неправильний старий пароль" });

            var newHash = _hasher.HashPassword(email, req.NewPassword);
            string updateSql = "UPDATE LOGIN SET pwd = @Pwd WHERE email = @Email;";
            await conn.ExecuteAsync(updateSql, new { Pwd = newHash, Email = email });

            return Ok(new { message = "Пароль успішно змінено" });
        }

        [Authorize] 
        [HttpGet("myinfo")]
        public async Task<IActionResult> GetMyProfile()
        {
            var email = User?.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "Потрібна авторизація" });

            using var conn = new MySqlConnection(_connectionString);

            var login = await conn.QueryFirstOrDefaultAsync<LoginUserDto>(
                "SELECT player_id FROM login WHERE email = @Email", new { Email = email });
            if (login == null || login.player_id == null)
                return NotFound(new { message = "Немає пов'язаних даних гравця" });

            var player = await conn.QueryFirstOrDefaultAsync<PlayerDetailsDto>(
                "SELECT pid, pname, dob, sex, photo FROM player WHERE pid = @Pid",
                new { Pid = login.player_id });

            if (player == null)
                return NotFound(new { message = "Гравець не знайдено" });


            player.PhotoBase64 = Utils.getAvatarBase64Photo(player.Photo);

            return Ok(player);
        }


        [Authorize]
        [HttpPost("myinfo/update")]
        public async Task<IActionResult> UpdateMyProfile([FromForm] PlayerEditDto dto)
        {
            var email = User?.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "Потрібна авторизація" });

            using var conn = new MySqlConnection(_connectionString);

            var login = await conn.QueryFirstOrDefaultAsync<LoginUserDto>(
                "SELECT player_id FROM login WHERE email = @Email", new { Email = email });

            if (login == null || login.player_id == null)
                return NotFound(new { message = "Немає пов'язаних даних гравця" });

            string sql = @"UPDATE player SET pname=@Name, dob=@Dob, sex=@Sex";
            string photoPart = "";
            byte[] photoBytes = null;
            if (dto.Photo != null && dto.Photo.Length > 0)
            {
                string Err = Utils.ValidateImage(dto.Photo);
                if (Err.Length > 0)
                    return BadRequest(new { message = Err });

                photoBytes = await Utils.FileToBytes(dto.Photo);
                photoPart = ", photo=@Photo";
            }

            sql = string.Format(sql, photoPart);
            sql += " WHERE pid = @Pid;";    

            await conn.ExecuteAsync(sql, new
            {
                Name = dto.Name,
                Dob = dto.Dob,
                Sex = dto.Sex,
                Pid = login.player_id,
                Photo = photoBytes
            });

            return Ok(new { message = "Зміни збережено!" });
        }

        private string CreateToken(string user, string Role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.Role, Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                //issuer: _configuration.GetSection("AuthOptions")["ISSUER"],
                //audience: _configuration.GetSection("AuthOptions")["AUDIENCE"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
