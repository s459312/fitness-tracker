using Bogus;
using FitnessTracker.Contracts.Request.Auth;
using FitnessTracker.Contracts.Request.Exercise;
using FitnessTracker.Contracts.Request.Training;
using FitnessTracker.Contracts.Request.User;
using FitnessTracker.Helpers;
using FitnessTracker.Models;

namespace FitnessTracker.IntegrationTests.Factories
{
    public static class Factory
    {

        public static class UserFactory
        {
            public static User GetModel(string role = "User")
            {
                int? roleId = 3;
                switch (role)
                {
                    case "Admin":
                        roleId = 1; break;
                    case "Moderator":
                        roleId = 2; break;
                    default:
                        roleId = 3; break;
                }
                
                var authHelper = new AuthHelper();
                authHelper.CreatePasswordHash("Password#2!", out byte[] passwordHash, out byte[] passwordSalt);
                return new Faker<User>()
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.Name, f => f.Person.FirstName)
                    .RuleFor(x => x.Surname, f => f.Person.LastName)
                    .RuleFor(x => x.PasswordHash, passwordHash)
                    .RuleFor(x => x.PasswordSalt, passwordSalt)
                    .RuleFor(x => x.RoleId, roleId)
                    .RuleFor(x => x.GoalId, 1)
                    .Generate();
            }

            public static UpdateUserRequest UpdateUserRequest()
            {
                return new Faker<UpdateUserRequest>()
                    .RuleFor(x => x.Name, f => f.Person.FirstName)
                    .RuleFor(x => x.Surname, f => f.Person.LastName)
                    .RuleFor(x => x.GoalId, 1)
                    .Generate();
            }
        }
        
        public static class Auth
        {
            public static AuthLoginRequest AuthLoginRequest(string email = "", string password = "")
            {
                var fakeData = new Faker<AuthLoginRequest>()
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.Password, "Password#2!")
                    .Generate();
                if (email != "")
                    fakeData.Email = email;
                if (password != "")
                    fakeData.Password = password;
                return fakeData;
            }

            public static AuthRegisterRequest AuthRegisterRequest()
            {
                return new Faker<AuthRegisterRequest>()
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.Password, "Password#2!")
                    .RuleFor(x => x.ConfirmPassword, "Password#2!")
                    .RuleFor(x => x.Name, f => f.Person.FullName)
                    .RuleFor(x => x.Surname, f => f.Person.LastName)
                    .RuleFor(x => x.GoalId, 1)
                    .Generate();
            }

            public static AuthChangePasswordRequest AuthChangePasswordRequest()
            {
                return new Faker<AuthChangePasswordRequest>()
                    .RuleFor(x => x.OldPassword, "Password#2!")
                    .RuleFor(x => x.NewPassword, "NewPassword#2!")
                    .RuleFor(x => x.ConfirmNewPassword, "NewPassword#2!")
                    .Generate();
            }
        }
        
        public static class Exercise
        {
            public static Models.Exercise GetModel()
            {
                return new Faker<Models.Exercise>()
                    .RuleFor(x => x.Name, f => f.Lorem.Word())
                    .RuleFor(x => x.Description, f => f.Lorem.Lines())
                    .RuleFor(x => x.GoalId, 1)
                    .RuleFor(x => x.Powtorzenia, f => f.Random.Int(1, 10))
                    .RuleFor(x => x.Serie, f => f.Random.Int(1, 10))
                    .RuleFor(x => x.Obciazenie, f => f.Random.Int(1, 10))
                    .RuleFor(x => x.Czas, 0)
                    .RuleFor(x => x.Dystans, 0)
                    .Generate();
            }
            
            public static CreateExerciseRequest CreateExerciseRequest()
            {
                return new Faker<CreateExerciseRequest>()
                    .RuleFor(x => x.Name, f => f.Lorem.Word())
                    .RuleFor(x => x.Description, f => f.Lorem.Lines())
                    .RuleFor(x => x.GoalId, 1)
                    .RuleFor(x => x.Powtorzenia, f => f.Random.Int(1, 10))
                    .RuleFor(x => x.Serie, f => f.Random.Int(1, 10))
                    .RuleFor(x => x.Obciazenie, f => f.Random.Int(1, 10))
                    .RuleFor(x => x.Czas, 0)
                    .RuleFor(x => x.Dystans, 0)
                    .Generate();
            }
            
            public static UpdateExerciseRequest UpdateExerciseRequest()
            {
                return new Faker<UpdateExerciseRequest>()
                    .RuleFor(x => x.Name, f => f.Lorem.Word())
                    .RuleFor(x => x.Description, f => f.Lorem.Lines())
                    .RuleFor(x => x.GoalId, 1)
                    .RuleFor(x => x.Powtorzenia, f => f.Random.Int(1, 10))
                    .RuleFor(x => x.Serie, f => f.Random.Int(1, 10))
                    .RuleFor(x => x.Obciazenie, f => f.Random.Int(1, 10))
                    .RuleFor(x => x.Czas, 0)
                    .RuleFor(x => x.Dystans, 0)
                    .Generate();
            }
        }

        public static class Training
        {
            public static Models.Training GetModel(bool isPublic = false)
            {
                return new Faker<Models.Training>()
                    .RuleFor(x => x.Name, f => f.Lorem.Word())
                    .RuleFor(x => x.Description, f => f.Lorem.Sentence())
                    .RuleFor(x => x.IsPublic, isPublic);
            }

            public static CreateTrainingRequest CreateTrainingRequest()
            {
                return new Faker<CreateTrainingRequest>()
                    .RuleFor(x => x.Name, f => f.Lorem.Word())
                    .RuleFor(x => x.Description, f => f.Lorem.Sentence());
            }
            
            public static UpdateTrainingRequest UpdateTrainingRequest()
            {
                return new Faker<UpdateTrainingRequest>()
                    .RuleFor(x => x.Name, f => f.Lorem.Word())
                    .RuleFor(x => x.Description, f => f.Lorem.Sentence());
            }
        }
    }
}