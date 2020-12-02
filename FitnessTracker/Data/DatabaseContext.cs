using FitnessTracker.Helpers;
using FitnessTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly IAuthHelper _authHelper;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IAuthHelper authHelper) : base(options)
        {
            _authHelper = authHelper;
        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Seeder.SeedRoles(modelBuilder);
            Seeder.SeedUsers(modelBuilder, _authHelper);
        }
    }    
    
}