using FitnessTracker.Data;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessTracker.Helpers
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IActionContextAccessor _actionContextAccessor;

        public AuthHelper() { }

        public AuthHelper(IActionContextAccessor actionContextAccessor)
        {
            _actionContextAccessor = actionContextAccessor;
        }

        public async Task<User> GetAuthenticatedUserModel(DatabaseContext context)
        {
            int userId = GetAuthenticatedUserId();
            if (userId > 0)
                return await context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            return null;
        }

        public int GetAuthenticatedUserId()
        {
            if (_actionContextAccessor.ActionContext.HttpContext.User == null)
                return 0;
            try
            {
                var claim = _actionContextAccessor.ActionContext.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == "id");
                return claim == null ? 0 : Convert.ToInt32(claim.Value);
            }
            catch
            {
                return 0;
            }
        }

        public string GetAuthenticatedUserRole()
        {
            if (_actionContextAccessor.ActionContext.HttpContext.User == null)
                return String.Empty;

            var role = _actionContextAccessor.ActionContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            return role == null ? "User" : role.Value;
        }

        public bool IsNormalUser()
        {
            return GetAuthenticatedUserRole() == "User";
        }

        public bool IsAdmin()
        {
            return GetAuthenticatedUserRole() == "Admin";
        }

        public bool IsEditor()
        {
            return GetAuthenticatedUserRole() == "Editor";
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}