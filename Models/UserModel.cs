namespace AuthApi.Models
{
    public class UserModel
    {
        public required string UserName { get; set; }
        public required string Email { get; set; } // Only needed for signup
        public required string Password { get; set; }
    }
}
