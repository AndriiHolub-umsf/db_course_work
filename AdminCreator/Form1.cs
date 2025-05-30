using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Ocsp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace AdminCreator
{
    public partial class Form1 : Form
    {
        public class UserInfo
        {
            public string Email { get; set; }
            public string Pwd { get; set; }
            public string Role { get; set; }
        }


        string _connectionString;
        public Form1()
        {
            InitializeComponent();

            var config = new ConfigurationBuilder()
                            .SetBasePath(AppContext.BaseDirectory)
                            .AddJsonFile("appsettings.json")
                            .Build();

            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        private async void CreatBtn_ClickAsync(object sender, EventArgs e)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            using var tx = await conn.BeginTransactionAsync();

            string Email = emailTxt.Text.Trim();
            string Password = pswTxt.Text;

            if(Password.Length < 8)
            {

                MessageBox.Show("Пароль має бути не менше 8 символів!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (Password != confirmTxt.Text)
            {
                MessageBox.Show("Паролі не збігаються!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            var adminExists = await conn.QueryFirstOrDefaultAsync<string>(
                "SELECT email FROM LOGIN WHERE role = 'Admin'", null, tx);

            string sql = "SELECT email,role FROM LOGIN WHERE email = @Email;";
            var user = await conn.QueryFirstOrDefaultAsync<UserInfo>(sql, new { Email = Email }, tx);
            if (user != null)
            {
                if(user.Role != "Admin")
                {
                    string updateSql = "UPDATE LOGIN SET role = 'Admin' WHERE email = @Email;";
                    await conn.ExecuteAsync(updateSql, new { Email });

                    MessageBox.Show("Користувач успішно оновлений до адміністратора!", "Інфо", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Адміністратор вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
    

            var hasher = new PasswordHasher<string>();
            var hash = hasher.HashPassword(Email, Password);

            await conn.ExecuteAsync(
                "INSERT INTO LOGIN (email, pwd, role) VALUES (@Email, @Pwd, 'Admin');",
                new { Email, Pwd = hash }, tx);

            await tx.CommitAsync();
            MessageBox.Show("Адміністратор успішно зареєстрований!", "Інфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
