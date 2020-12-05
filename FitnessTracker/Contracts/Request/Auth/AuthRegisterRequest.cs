namespace FitnessTracker.Contracts.Request.Auth
{
    public class AuthRegisterRequest
    {
        /// <summary>
        /// Imię nowego użytkownika
        /// </summary>
        /// <example>Example Name</example>
        public string Name { get; set; }

        /// <summary>
        /// Nazwisko nowego użytkownika
        /// </summary>
        /// <example>Example Surname</example>
        public string Surname { get; set; }

        /// <summary>
        /// Email nowego użytkownika
        /// </summary>
        /// <example>testuser@gmail.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Hasło nowego użytkownika
        /// </summary>
        /// <example>Password#2!</example>
        public string Password { get; set; }

        /// <summary>
        /// Potwierdzenie hasła nowego użytkownika
        /// </summary>
        /// <example>Password#2!</example>
        public string ConfirmPassword { get; set; }
        
        /// <summary>
        /// Id celu użytkownika
        /// </summary>
        /// <example>1</example>
        public int GoalId { get; set; }
    }
}