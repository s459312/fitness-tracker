using System.Threading.Tasks;
using FitnessTracker.Contracts.Request.Auth;
using FitnessTracker.Models;

namespace FitnessTracker.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> RegisterAsync(AuthRegisterRequest request);
        Task<AuthenticationResult> LoginAsync(AuthLoginRequest request);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        Task<AuthenticationResult> ChangePasswordAsync(AuthChangePasswordRequest request);
    }
}