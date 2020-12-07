namespace FitnessTracker.Contracts.Response.Role
{
    public class RoleResponse
    {
        /// <summary>
        /// Id roli
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nazwa roli
        /// </summary>
        /// <example>Admin</example>
        public string Name { get; set; }
    }
}