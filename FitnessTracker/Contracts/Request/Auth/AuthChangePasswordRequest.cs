namespace FitnessTracker.Contracts.Request.Auth
{
    public class AuthChangePasswordRequest
    {
        /// <summary>
        /// Stare Hasło użytkownika
        /// </summary>
        /// <example>Password#2!</example>
        public string OldPassword { get; set; }
        
        /// <summary>
        /// Nowe hasło użytkownika
        /// </summary>
        /// <example>NewPassword#2!</example>
        public string NewPassword { get; set; }
        
        /// <summary>
        /// Potwierdzenie nowego hasła użytkownika
        /// </summary>
        /// <example>NewPassword#2!</example>
        public string ConfirmNewPassword { get; set; }
    }
}