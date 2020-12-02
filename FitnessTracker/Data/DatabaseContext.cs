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

        public DbSet<Coach> Coach { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<Goal> Goal { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<HistoryStats> HistoryStats { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<UserTraining> UserTraining { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTraining>()
                .HasKey(c => new { c.IdUser, c.IdTraining });
            modelBuilder.Entity<ExerciseTraining>()
                .HasKey(c => new { c.IdExercise, c.IdTraining });
            modelBuilder.Entity<History>()
                .HasKey(c => new { c.IdUser, c.IdExercise, c.Date });

            Seeder.SeedRoles(modelBuilder);
            Seeder.SeedGoals(modelBuilder);
            Seeder.SeedUsers(modelBuilder, _authHelper);

        }
    }    
    
}