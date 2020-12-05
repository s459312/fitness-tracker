using FitnessTracker.Contracts.Response.Goal;
using FitnessTracker.Contracts.Response.Role;

namespace FitnessTracker.Contracts.Response.User
{
    public class UserResponse
    {
        /// <summary>
        /// Id użytkownika
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        
        /// <summary>
        /// Email użytkownika
        /// </summary>
        /// <example>user@example.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Imię użytkownika
        /// </summary>
        /// <example>Example Name</example>
        public string Name { get; set; }

        /// <summary>
        /// Nazwisko użytkownika
        /// </summary>
        /// <example>Example surname</example>
        public string Surname { get; set; }

        public RoleResponse Role { get; set; }
        
        public GoalResponse Goal { get; set; }
    }
}