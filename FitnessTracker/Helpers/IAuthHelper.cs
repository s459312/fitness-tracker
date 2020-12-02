using System.Threading.Tasks;
using FitnessTracker.Data;
using FitnessTracker.Models;

namespace FitnessTracker.Helpers
{
    public interface IAuthHelper
    {
        public Task<User> GetAuthenticatedUserModel(DatabaseContext context);
        public int GetAuthenticatedUserId();
        public string GetAuthenticatedUserRole();
        bool IsNormalUser();
        bool IsAdmin();
        bool IsEditor();
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}