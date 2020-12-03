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
                new Coach { Id = 1, Email = "coach_1@example.com", Name = "CoachName_1", Surname = "CoachSurname_1", Phone = "123456789", GoalId = 1 },
                new Coach { Id = 2, Email = "coach_2@example.com", Name = "CoachName_2", Surname = "CoachSurname_2", Phone = "987654321", GoalId = 2 }
            );
        }

        public static void SeedExercise(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { Id = 1, Name = "Exercise_1", Serie = 1, Powtorzenia = 1, GoalId = 1 },
                new Exercise { Id = 2, Name = "Exercise_2", Serie = 2, Powtorzenia = 2, GoalId = 2 }
            );
        }

        public static void SeedTraining(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Training>().HasData(
                new Training { Id = 1, IsPublic = true, Name = "Trening_Publiczny_1" },
                new Training { Id = 2, IsPublic = false, Name = "Trening_Prywatny_1" },
                new Training { Id = 3, IsPublic = false, Name = "Trening_Prywatny_2" }
            );

            modelBuilder.Entity<TrainingExercise>().HasData(
                new TrainingExercise { ExerciseId = 1, TrainingId = 1 },
                new TrainingExercise { ExerciseId = 2, TrainingId = 1 },
                new TrainingExercise { ExerciseId = 1, TrainingId = 2 },
                new TrainingExercise { ExerciseId = 1, TrainingId = 3 },
                new TrainingExercise { ExerciseId = 2, TrainingId = 3 }
            );

            modelBuilder.Entity<UserTraining>().HasData(
                new UserTraining { UserId = 1, TrainingId = 1, Favourite = false },
                new UserTraining { UserId = 1, TrainingId = 2, Favourite = false },
                new UserTraining { UserId = 1, TrainingId = 3, Favourite = true }
            );
        }

        public static void SeedHistory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>().HasData(
                new History { Id = 1, UserId = 1, ExerciseId = 1, Date = DateTime.Parse("2020-12-03") },
                new History { Id = 2, UserId = 1, ExerciseId = 2, Date = DateTime.Parse("2020-12-03") }
            );

            modelBuilder.Entity<HistoryStats>().HasData(
                new HistoryStats { Id = 1, Serie = 1, Powtorzenia = 1, HistoryId = 1 },
                new HistoryStats { Id = 2, Serie = 2, Powtorzenia = 2, HistoryId = 2 }
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