using AuthApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync();

            string query = "SELECT COUNT(*) FROM Users WHERE UserName = @username AND Password = @password";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            int count = (int)await cmd.ExecuteScalarAsync();
            return count > 0;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync();

            string query = "SELECT COUNT(*) FROM Users WHERE UserName = @username";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@username", username);

            int count = (int)await cmd.ExecuteScalarAsync();
            return count > 0;
        }

        public async Task<bool> RegisterUserAsync(UserModel user)
        {
            using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync();

            string query = "INSERT INTO Users (UserName, Email, Password) VALUES (@username, @email, @password)";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);

            int rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }
    }
}
