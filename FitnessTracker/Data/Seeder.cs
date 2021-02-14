using FitnessTracker.Helpers;
using FitnessTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FitnessTracker.Data
{
    public static class Seeder
    {
        public static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Moderator" },
                new Role { Id = 3, Name = "User" }
            );
        }

        public static void SeedGoals(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goal>().HasData(
                new Goal { Id = 1, Name = "Redukcja tkanki tłuszczowej" },
                new Goal { Id = 2, Name = "Przybranie masy mięśniowej" },
                new Goal { Id = 3, Name = "Rekompozycja sylwetki" }
            );
        }

        public static void SeedCoach(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coach>().HasData(
                new Coach { Id = 1, Email = "coach_1@example.com", Name = "CoachName_1", Surname = "CoachSurname_1", Phone = "123456789", Description = "Hello, I'm a best coach ever", GoalId = 1 },
                new Coach { Id = 2, Email = "coach_2@example.com", Name = "CoachName_2", Surname = "CoachSurname_2", Phone = "987654321", Description = "Hi, I'm John Kowalski and with me, you'll be a strongman!", GoalId = 2 }
            );
        }

        public static void SeedExercise(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { Id = 1, Name = "Przysiad ze sztangą", Serie = 4, Powtorzenia = 8, GoalId = 1, Description = "Zrób przysiad trzymająć sztangę" },
                new Exercise { Id = 2, Name = "Wykroki ze sztangielkami", Serie = 4, Powtorzenia = 8, GoalId = 3, Description = "Zrób wykrok ze sztangielkami" },
                new Exercise { Id = 3, Name = "Przysiad w szerokim rozkroku", Serie = 4, Powtorzenia = 8, GoalId = 3 },
                new Exercise { Id = 4, Name = "Wyciskanie sztangielek w pozycji leżącej", Serie = 3, Powtorzenia = 12, GoalId = 3 },
                new Exercise { Id = 5, Name = "Brzuszki z nogami uniesionymi", Serie = 3, Powtorzenia = 25, GoalId = 3 },
                new Exercise { Id = 6, Name = "Brzuszki skośne", Serie = 3, Powtorzenia = 25, GoalId = 3 },
                new Exercise { Id = 7, Name = "Plank", Serie = 3, Czas = 30, GoalId = 3 }
            );
        }

        public static void SeedTraining(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Training>().HasData(
                new Training { Id = 1, IsPublic = true, Name = "Trening_Publiczny_1" },
                new Training { Id = 2, IsPublic = false, Name = "Trening_Prywatny_1" },
                new Training { Id = 3, IsPublic = false, Name = "Trening_Prywatny_2" },
                new Training { Id = 4, IsPublic = true, Name = "Trening_Publiczny_2" }
            );

            modelBuilder.Entity<TrainingExercise>().HasData(
                new TrainingExercise { ExerciseId = 1, TrainingId = 1 },
                new TrainingExercise { ExerciseId = 2, TrainingId = 1 },
                new TrainingExercise { ExerciseId = 3, TrainingId = 1 },
                new TrainingExercise { ExerciseId = 4, TrainingId = 1 },
                new TrainingExercise { ExerciseId = 1, TrainingId = 2 },
                new TrainingExercise { ExerciseId = 5, TrainingId = 2 },
                new TrainingExercise { ExerciseId = 6, TrainingId = 2 },
                new TrainingExercise { ExerciseId = 1, TrainingId = 3 },
                new TrainingExercise { ExerciseId = 2, TrainingId = 3 },
                new TrainingExercise { ExerciseId = 6, TrainingId = 3 },
                new TrainingExercise { ExerciseId = 7, TrainingId = 3 },
                new TrainingExercise { ExerciseId = 4, TrainingId = 4 },
                new TrainingExercise { ExerciseId = 3, TrainingId = 4 },
                new TrainingExercise { ExerciseId = 2, TrainingId = 4 },
                new TrainingExercise { ExerciseId = 5, TrainingId = 4 }
            );

            modelBuilder.Entity<UserTraining>().HasData(
                new UserTraining { UserId = 1, TrainingId = 1, Favourite = false },
                new UserTraining { UserId = 1, TrainingId = 2, Favourite = false },
                new UserTraining { UserId = 1, TrainingId = 3, Favourite = true },
                new UserTraining { UserId = 2, TrainingId = 3, Favourite = false },
                new UserTraining { UserId = 3, TrainingId = 3, Favourite = false },
                new UserTraining { UserId = 3, TrainingId = 4, Favourite = true }
            );

            modelBuilder.Entity<TrainingHistory>().HasData(
                new TrainingHistory { Id = 1, UserId = 1, TrainingId = 3, Date = DateTime.Parse("2020-12-05") },
                new TrainingHistory { Id = 2, UserId = 2, TrainingId = 3, Date = DateTime.Parse("2020-12-03") },
                new TrainingHistory { Id = 3, UserId = 2, TrainingId = 3, Date = DateTime.Parse("2020-12-04") },
                new TrainingHistory { Id = 4, UserId = 1, TrainingId = 1, Date = DateTime.Parse("2020-12-03") },
                new TrainingHistory { Id = 5, UserId = 1, TrainingId = 2, Date = DateTime.Parse("2020-12-04") }
            );
        }

        public static void SeedHistory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseHistory>().HasData(
                new ExerciseHistory { Id = 1, UserId = 1, ExerciseId = 1, Date = DateTime.Parse("2020-12-03") },
                new ExerciseHistory { Id = 2, UserId = 1, ExerciseId = 2, Date = DateTime.Parse("2020-12-03") },
                new ExerciseHistory { Id = 3, UserId = 1, ExerciseId = 1, Date = DateTime.Parse("2020-12-04") },
                new ExerciseHistory { Id = 4, UserId = 1, ExerciseId = 2, Date = DateTime.Parse("2020-12-04") }
            );

            modelBuilder.Entity<ExerciseHistoryStats>().HasData(
                new ExerciseHistoryStats { Id = 1, Serie = 1, Powtorzenia = 1, ExerciseHistoryId = 1 },
                new ExerciseHistoryStats { Id = 2, Serie = 2, Powtorzenia = 4, ExerciseHistoryId = 1 },
                new ExerciseHistoryStats { Id = 3, Serie = 3, Powtorzenia = 6, ExerciseHistoryId = 1 },
                new ExerciseHistoryStats { Id = 4, Serie = 1, Powtorzenia = 3, ExerciseHistoryId = 2 },
                new ExerciseHistoryStats { Id = 5, Serie = 2, Powtorzenia = 6, ExerciseHistoryId = 2 },
                new ExerciseHistoryStats { Id = 6, Serie = 3, Powtorzenia = 8, ExerciseHistoryId = 2 },
                new ExerciseHistoryStats { Id = 7, Serie = 7, Powtorzenia = 9, ExerciseHistoryId = 3 },
                new ExerciseHistoryStats { Id = 8, Serie = 2, Powtorzenia = 2, ExerciseHistoryId = 3 },
                new ExerciseHistoryStats { Id = 9, Serie = 8, Powtorzenia = 1, ExerciseHistoryId = 4 },
                new ExerciseHistoryStats { Id = 10, Serie = 4, Powtorzenia = 5, ExerciseHistoryId = 4 }
            );
        }

        public static void SeedUsers(ModelBuilder modelBuilder, IAuthHelper authHelper)
        {
            authHelper.CreatePasswordHash("Password#2!", out byte[] passwordHash, out byte[] passwordSalt);

            User adminUser = new User
            {
                Id = 1,
                Email = "admin@gmail.com",
                Name = "Admin Name",
                Surname = "Admin Surname",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 1,
                GoalId = 2
            };

            User moderatorUser = new User
            {
                Id = 2,
                Email = "moderator@gmail.com",
                Name = "Moderator Name",
                Surname = "Moderator Surname",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 2,
                GoalId = 3
            };

            User user = new User
            {
                Id = 3,
                Email = "user@gmail.com",
                Name = "User Name",
                Surname = "User Surname",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 3,
                GoalId = 1
            };

            modelBuilder.Entity<User>().HasData(
                adminUser,
                moderatorUser,
                user
            );
        }
    }
}