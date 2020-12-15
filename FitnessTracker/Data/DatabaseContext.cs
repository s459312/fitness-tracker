using FitnessTracker.Helpers;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FitnessTracker.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly IAuthHelper _authHelper;
        private readonly IWebHostEnvironment _environment;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IAuthHelper authHelper, IWebHostEnvironment environment) : base(options)
        {
            _authHelper = authHelper;
            _environment = environment;
        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Coach> Coach { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<Goal> Goal { get; set; }
        public DbSet<ExerciseHistory> ExerciseHistory { get; set; }
        public DbSet<TrainingHistory> TrainingHistory { get; set; }
        public DbSet<ExerciseHistoryStats> ExerciseHistoryStats { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<UserTraining> UserTraining { get; set; }
        public DbSet<TrainingExercise> TrainingExercise { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*
             * USER
             * Usunięcie użytkownika powoduje
             *     - usunięcie przypisanych do niego treningów ( tabela UserTraining )
             *     - usunięcie przypisanych do niego historii ( tabela History )
             *     - usunięcie przypisanych do niego statystyk historii ( tabela HistoryStats )
             * Usunięcie użytkownika NIE powoduje
             *     - usunięcia stworzonych przez niego treningów ( tabela Training )
             *       trzeb je usunąć ręcznie przed usunięciem uzytkownika  
             */

            /*
             * EXERCISE
             * Nie można usunąć ćwiczenia jeżeli jest przypisane do jakiegoś treningu lub historii
             */

            /*
             * GOAL
             * Nie można usunąć celu jeżeli jest przypisany do jakiegoś
             *     ćwiczenia lub trenera lub użytkownika
             */

            modelBuilder.Entity<UserTraining>()
                .HasKey(c => new { c.UserId, c.TrainingId });

            modelBuilder.Entity<TrainingExercise>()
                .HasKey(c => new { c.ExerciseId, c.TrainingId });

            modelBuilder.Entity<ExerciseHistory>()
                .HasIndex(p => new { p.ExerciseId, p.UserId, p.Date }).IsUnique();

            /*
            modelBuilder.Entity<History>()
                .HasKey(c => new { c.IdUser, c.IdExercise, c.Date });*/
            /*
            // Blokuje usuwanie ćwiczenia jeżeli jest przypisane przynajmniej do jednej historii
            modelBuilder.Entity<History>()
                .HasOne(x => x.Exercise)
                .WithMany(x => x.Histories)
                .HasForeignKey(x => x.IdExercise)
                .OnDelete(DeleteBehavior.Restrict);
            */
            // Przy usuwaniu użytkownika usuwa wszystkie statystyki przywiązane do jego historii ćwiczeń
            /*
            modelBuilder.Entity<History>()
                .HasMany(x => x.HistoryStats)
                .WithOne(x => x.History)
                .HasForeignKey(x => new {x.HistoryIdUser, x.HistoryIdExercise, x.HistoryDate})
                .OnDelete(DeleteBehavior.Cascade);
            */
            Seeder.SeedRoles(modelBuilder);
            Seeder.SeedGoals(modelBuilder);
            Seeder.SeedUsers(modelBuilder, _authHelper);

            if (!_environment.IsEnvironment("Test"))
            {
                Seeder.SeedCoach(modelBuilder);
                Seeder.SeedExercise(modelBuilder);
                Seeder.SeedTraining(modelBuilder);
                Seeder.SeedHistory(modelBuilder);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }

}
