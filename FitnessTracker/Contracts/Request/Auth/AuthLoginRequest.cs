using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Contracts.Request.Auth
{
    public class AuthLoginRequest
    {
        /// <summary>
        /// Adres Email użytkownika
        /// </summary>
        /// <example>admin@gmail.com</example>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Hasło użytkownika
        /// </summary>
        /// <example>Password#2!</example>
        public string Password { get; set; }
    }
}