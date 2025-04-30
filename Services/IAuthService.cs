using AuthApi.Models;

namespace AuthApi.Services
{
    public interface IAuthService
    {
        Task<bool> ValidateUserAsync(string username, string password);
        Task<bool> RegisterUserAsync(UserModel user);
        Task<bool> UserExistsAsync(string username);
    }
}
