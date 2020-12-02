
namespace FitnessTracker.Models
{
    public class AuthenticationResult
    {
        public string Token { get; set; } = null;
        public string RefreshToken { get; set; }
        public bool Success { get; set; } = false;
        public string Error { get; set; }
    }
}