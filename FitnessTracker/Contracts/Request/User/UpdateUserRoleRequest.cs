namespace FitnessTracker.Contracts.Request.User
{
    public class UpdateUserRoleRequest
    {
        /// <summary>
        /// Id nowej roli użytkownika
        /// </summary>
        /// <example>1</example>
        public int RoleId { get; set; }
    }
}