using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FitnessTracker.Contracts;
using FitnessTracker.Contracts.Request.Training;
using FitnessTracker.Contracts.Response.Training;
using FitnessTracker.IntegrationTests.Domain;
using FitnessTracker.IntegrationTests.Factories;
using FitnessTracker.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace FitnessTracker.IntegrationTests.Tests
{
    public class TrainingControllerTest : IntegrationTestCore
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TrainingControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Controller__Unauthenticated_User_Cant_Access_Endpoints()
        {
            foreach (PropertyInfo propertyInfo in typeof(ApiRoutes.Training).GetProperties())
            {
                var response = await Client.GetAsync(propertyInfo.GetValue(null).ToString());
                response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            }
        }
        
        [Fact]
        public async Task GetAll__Authenticated_User_Can_Retrieve_All_Assigned_trainings()
        {
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            Training training1 = await CreatePrivateTraining(user.Id);
            Training training2 = await CreatePrivateTraining(user.Id);
            Training training3 = await CreatePublicTraining(user.Id);

            var response = await Client.GetAsync(ApiRoutes.Training.GetAllUserTrainings);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var body = await response.Content.ReadAsAsync<List<TrainingMinifiedResponse>>();

            body.Count.Should().Be(3);
            body.ForEach(x => x.Favourite.Should().Be(false));
            body[0].Id.Should().Be(training1.Id);
            body[1].Id.Should().Be(training2.Id);
            body[2].Id.Should().Be(training3.Id);
        }
        
        [Fact]
        public async Task GetAll__Authenticated_User_Can_Retrieve_All_Available_Public_Trainings()
        {
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            Training training1 = await CreatePublicTraining();
            Training training2 = await CreatePublicTraining();
            Training training3 = await CreatePublicTraining(user.Id);

            var response = await Client.GetAsync(ApiRoutes.Training.GetAllAvailableUserPublicTrainings);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var body = await response.Content.ReadAsAsync<List<PublicTrainingResponse>>();

            body.Count.Should().Be(2);
            body[0].Id.Should().Be(training1.Id);
            body[1].Id.Should().Be(training2.Id);
        }
        
        [Fact]
        public async Task GetAll__Authenticated_Admin_Or_Moderator_Can_Retrieve_All_Public_Trainings()
        {
            AuthenticatedUser user = await LogInAs("admin@gmail.com");
            Training training1 = await CreatePublicTraining();
            Training training2 = await CreatePublicTraining();
            Training training3 = await CreatePublicTraining(user.Id);

            var response = await Client.GetAsync(ApiRoutes.Training.GetAllPublicTrainings);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var body = await response.Content.ReadAsAsync<List<PublicTrainingResponse>>();

            body.Count.Should().Be(3);
            body[0].Id.Should().Be(training1.Id);
            body[1].Id.Should().Be(training2.Id);
            body[2].Id.Should().Be(training3.Id);
        }
        
        [Fact]
        public async Task Get__Authenticated_User_Can_Retrieve_Full_Info_Only_About_His_Or_Public_Training()
        {
            AuthenticatedUser admin = await LogInAs("admin@gmail.com");
            Training training1 = await CreatePrivateTraining(admin.Id);
            Training training2 = await CreatePublicTraining();
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            Training training3 = await CreatePublicTraining(user.Id);
            Training training4 = await CreatePrivateTraining(user.Id);
            
            string adminTrainingEndpoint = ApiRoutes.Training.Get.Replace("{trainingId}", training1.Id.ToString());
            string publicTrainingEndpoint = ApiRoutes.Training.Get.Replace("{trainingId}", training2.Id.ToString());
            string userPublicTrainingEndpoint = ApiRoutes.Training.Get.Replace("{trainingId}", training3.Id.ToString());
            string userTrainingEndpoint = ApiRoutes.Training.Get.Replace("{trainingId}", training4.Id.ToString());
            
            var response = await Client.GetAsync(adminTrainingEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            
            response = await Client.GetAsync(publicTrainingEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            
            response = await Client.GetAsync(userPublicTrainingEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var body = await response.Content.ReadAsAsync<TrainingFullResponse>();
            body.Id.Should().Be(training3.Id);
            
            response = await Client.GetAsync(userTrainingEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            body = await response.Content.ReadAsAsync<TrainingFullResponse>();
            body.Id.Should().Be(training4.Id);
        }
        
        [Fact]
        public async Task Create__User_Can_Create_Private_Training()
        {
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            CreateTrainingRequest createRequest = Factory.Training.CreateTrainingRequest();
            
            var response = await Client.PostAsJsonAsync(ApiRoutes.Training.Create, createRequest);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            
            var responseData = await response.Content.ReadAsAsync<TrainingMinifiedResponse>();
            var dbTraining = await Context.Training.FirstOrDefaultAsync(x => x.Id == responseData.Id);
            var dbUserTraining = await Context.UserTraining
                .FirstOrDefaultAsync(x => x.TrainingId == responseData.Id && x.UserId == user.Id);

            dbTraining.Should().NotBeNull();
            dbTraining.Name.Should().Be(responseData.Name);
            dbTraining.IsPublic.Should().Be(false);

            dbUserTraining.Should().NotBeNull();

            int trainingCount = await Context.Training.AsNoTracking().CountAsync();
            trainingCount.Should().Be(1);
            int userTrainingCount = await Context.UserTraining.AsNoTracking().CountAsync();
            userTrainingCount.Should().Be(1);
        }
        
        [Fact]
        public async Task Create__Admin_Or_Moderator_Can_Create_Public_Training()
        {
            string[] users = {"admin@gmail.com", "moderator@gmail.com"};
            int i = 1;
            foreach (var user in users)
            {
                await LogInAs(user);
                CreateTrainingRequest createRequest = Factory.Training.CreateTrainingRequest();
            
                var response = await Client.PostAsJsonAsync(ApiRoutes.Training.CreatePublic, createRequest);
                response.StatusCode.Should().Be(HttpStatusCode.Created);
            
                var responseData = await response.Content.ReadAsAsync<TrainingMinifiedResponse>();
                var dbTraining = await Context.Training.FirstOrDefaultAsync(x => x.Id == responseData.Id);

                dbTraining.Should().NotBeNull();
                dbTraining.Name.Should().Be(responseData.Name);
                dbTraining.IsPublic.Should().Be(true);

                int trainingCount = await Context.Training.AsNoTracking().CountAsync();
                trainingCount.Should().Be(i);
                int userTrainingCount = await Context.UserTraining.AsNoTracking().CountAsync();
                userTrainingCount.Should().Be(0);
                i++;
            }
        }

        [Fact]
        public async Task Patch__User_Can_Assign_Public_Training_To_His_Trainings()
        {
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            Training training = await CreatePublicTraining();
            AddPublicTrainingToUserRequest request = new AddPublicTrainingToUserRequest {TrainingId = training.Id};
            
            HttpContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await Client.PatchAsync(ApiRoutes.Training.AssignPublicTrainingToUser, content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            int userTrainingCount = await Context.UserTraining.AsNoTracking().CountAsync();
            userTrainingCount.Should().Be(1);
        }

        [Fact]
        public async Task Put__User_Can_Update_His_Private_Training()
        {
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            Training training = await CreatePrivateTraining(user.Id);
            UpdateTrainingRequest request = Factory.Training.UpdateTrainingRequest();
            string updateEndpoint = ApiRoutes.Training.Update.Replace("{trainingId}", training.Id.ToString());
            
            var response = await Client.PutAsJsonAsync(updateEndpoint, request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var updatedTraining = await Context.Training
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == training.Id);

            updatedTraining.Name.Should().Be(request.Name);
            updatedTraining.Description.Should().Be(request.Description);
            
            int trainingCount = await Context.Training.AsNoTracking().CountAsync();
            trainingCount.Should().Be(1);
            int userTrainingCount = await Context.UserTraining.AsNoTracking().CountAsync();
            userTrainingCount.Should().Be(1);
        }
        
        [Fact]
        public async Task Put__User_Cant_Update_Other_User_Private_Training()
        {
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            Training training = await CreatePrivateTraining(user.Id);
            AuthenticatedUser admin = await LogInAs("admin@gmail.com");
            UpdateTrainingRequest request = Factory.Training.UpdateTrainingRequest();
            string updateEndpoint = ApiRoutes.Training.Update.Replace("{trainingId}", training.Id.ToString());
            
            var response = await Client.PutAsJsonAsync(updateEndpoint, request);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task Put__Admin_Or_Moderator_Can_Update_Public_Training()
        {
            string[] users = {"admin@gmail.com", "moderator@gmail.com"};
            int i = 1;
            foreach (var user in users)
            {
                await LogInAs(user);
                Training training = await CreatePublicTraining();
                UpdateTrainingRequest request = Factory.Training.UpdateTrainingRequest();
                string updateEndpoint = ApiRoutes.Training.UpdatePublic.Replace("{trainingId}", training.Id.ToString());
            
                var response = await Client.PutAsJsonAsync(updateEndpoint, request);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            
                var updatedTraining = await Context.Training
                    .AsNoTracking().FirstOrDefaultAsync(x => x.Id == training.Id);

                updatedTraining.Name.Should().Be(request.Name);
                updatedTraining.Description.Should().Be(request.Description);
            
                int trainingCount = await Context.Training.AsNoTracking().CountAsync();
                trainingCount.Should().Be(i);
                i++;
            }
        }
        
        [Fact]
        public async Task Delete__User_Can_Delete_His_Private_Training()
        {
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            Training training = await CreatePrivateTraining(user.Id);
            string deleteEndpoint = ApiRoutes.Training.Delete.Replace("{trainingId}", training.Id.ToString());
            
            var response = await Client.DeleteAsync(deleteEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            int trainingCount = await Context.Training.AsNoTracking().CountAsync();
            trainingCount.Should().Be(0);
            int userTrainingCount = await Context.UserTraining.AsNoTracking().CountAsync();
            userTrainingCount.Should().Be(0);
        }
        
        [Fact]
        public async Task Delete__User_Can_Delete_His_Public_Training()
        {
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            Training training = await CreatePublicTraining(user.Id);
            string deleteEndpoint = ApiRoutes.Training.Delete.Replace("{trainingId}", training.Id.ToString());
            
            var response = await Client.DeleteAsync(deleteEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            int trainingCount = await Context.Training.AsNoTracking().CountAsync();
            trainingCount.Should().Be(1);
            int userTrainingCount = await Context.UserTraining.AsNoTracking().CountAsync();
            userTrainingCount.Should().Be(0);
        }
        
        [Fact]
        public async Task Delete__User_Cant_Delete_Other_User_Training()
        {
            AuthenticatedUser user = await LogInAs("admin@gmail.com");
            Training training = await CreatePrivateTraining(user.Id);
            await LogInAs("user@gmail.com");
            string deleteEndpoint = ApiRoutes.Training.Delete.Replace("{trainingId}", training.Id.ToString());
            
            var response = await Client.DeleteAsync(deleteEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task Delete__Admin_Or_Moderator_Can_Delete_Public_Training()
        {
            string[] users = {"admin@gmail.com", "moderator@gmail.com"};
            int i = 1;
            foreach (var user in users)
            {
                await LogInAs(user);
                Training training = await CreatePublicTraining();
                string deleteEndpoint = ApiRoutes.Training.DeletePublic.Replace("{trainingId}", training.Id.ToString());
            
                var response = await Client.DeleteAsync(deleteEndpoint);
                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
                int trainingCount = await Context.Training.AsNoTracking().CountAsync();
                trainingCount.Should().Be(0);
            }
        }

        [Fact]
        public async Task Patch__User_Can_Add_Exercises_To_His_Private_Training()
        {
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            Training training = await CreatePrivateTraining(user.Id);
            Exercise exercise1 = await CreateExercise();
            Exercise exercise2 = await CreateExercise();
            string patchEndpoint = ApiRoutes.Training.AddExercisesToTraining.Replace("{trainingId}", training.Id.ToString());
            UpdateTrainingExercisesRequest request = new UpdateTrainingExercisesRequest
            {
                Exercises = new[] {exercise1.Id, exercise2.Id}
            };
            
            HttpContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await Client.PatchAsync(patchEndpoint, content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Patch__User_Cant_Add_Exercises_To_His_Public_Training()
        {
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            Training training = await CreatePublicTraining(user.Id);
            Exercise exercise1 = await CreateExercise();
            string patchEndpoint = ApiRoutes.Training.AddExercisesToTraining.Replace("{trainingId}", training.Id.ToString());
            UpdateTrainingExercisesRequest request = new UpdateTrainingExercisesRequest
            {
                Exercises = new[] {exercise1.Id}
            };
            
            HttpContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await Client.PatchAsync(patchEndpoint, content);
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        
        [Fact]
        public async Task Patch__Admin_Or_Moderator_Can_Add_Exercises_To_Public_Training()
        {
            string[] users = {"admin@gmail.com", "moderator@gmail.com"};
            foreach (var user in users)
            {
                await LogInAs(user);
                Training training = await CreatePublicTraining();
                Exercise exercise1 = await CreateExercise();
                string patchEndpoint = ApiRoutes.Training.AddExercisesToPublicTraining.Replace("{trainingId}", training.Id.ToString());
                UpdateTrainingExercisesRequest request = new UpdateTrainingExercisesRequest
                {
                    Exercises = new[] {exercise1.Id}
                };
            
                HttpContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await Client.PatchAsync(patchEndpoint, content);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
        
        [Fact]
        public async Task Patch_User_Can_Add_Training_To_History()
        {
            AuthenticatedUser user = await LogInAs("user@gmail.com");
            
            var request = await CreateAddTrainingToHistoryRequest(user);

            HttpContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await Client.PatchAsync(ApiRoutes.Training.AddTrainingToHistory, content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            int trainingHistoryCount = await Context.TrainingHistory.AsNoTracking().CountAsync();
            trainingHistoryCount.Should().Be(1);
            
            int exerciseHistoryCount = await Context.ExerciseHistory.AsNoTracking().CountAsync();
            exerciseHistoryCount.Should().Be(2);
            
            int exerciseHistoryStatsCount = await Context.ExerciseHistoryStats.AsNoTracking().CountAsync();
            exerciseHistoryStatsCount.Should().Be(2);
        }
        
        private async Task<Training> CreatePublicTraining(int userId = 0)
        {
            Training trainingModel = Factory.Training.GetModel(true);
            await Context.Training.AddAsync(trainingModel);
            await Context.SaveChangesAsync();

            if (userId > 0)
            {
                UserTraining userTraining = new UserTraining
                {
                    TrainingId = trainingModel.Id,
                    UserId = userId
                };
                await Context.UserTraining.AddAsync(userTraining);
                await Context.SaveChangesAsync();
            }
            
            return trainingModel;
        }
        
        private async Task<Training> CreatePrivateTraining(int userId)
        {
            Training trainingModel = Factory.Training.GetModel(false);
            await Context.Training.AddAsync(trainingModel);
            await Context.SaveChangesAsync();

            UserTraining userTraining = new UserTraining
            {
                TrainingId = trainingModel.Id,
                UserId = userId
            };
            await Context.UserTraining.AddAsync(userTraining);
            await Context.SaveChangesAsync();
            
            return trainingModel;
        }
        
        private async Task<Exercise> CreateExercise()
        {
            Exercise createdExercise = Factory.Exercise.GetModel();
            await Context.Exercise.AddAsync(createdExercise);
            await Context.SaveChangesAsync();
            return createdExercise;
        }
        
        private async Task<AddTrainingToHistoryRequest> CreateAddTrainingToHistoryRequest(AuthenticatedUser user)
        {
            Training training = await CreatePrivateTraining(user.Id);
            Exercise exercise1 = await CreateExercise();
            Exercise exercise2 = await CreateExercise();

            TrainingExercise trainingExercise1 = new TrainingExercise {ExerciseId = exercise1.Id, TrainingId = training.Id};
            TrainingExercise trainingExercise2 = new TrainingExercise {ExerciseId = exercise2.Id, TrainingId = training.Id};

            await Context.TrainingExercise.AddRangeAsync(new List<TrainingExercise> {trainingExercise1, trainingExercise2});
            await Context.SaveChangesAsync();

            AddTrainingToHistoryRequest request = new AddTrainingToHistoryRequest
            {
                TrainingId = training.Id,
                Exercises = new List<ExerciseHistoryRequest>
                {
                    new ExerciseHistoryRequest
                    {
                        ExerciseId = exercise1.Id,
                        ExerciseHistoryStat = new ExerciseHistoryStatsRequest
                        {
                            Obciazenie = 20,
                            Powtorzenia = 10,
                            Serie = 3,
                            Czas = 0,
                            Dystans = 0
                        }
                    },
                    new ExerciseHistoryRequest
                    {
                        ExerciseId = exercise2.Id,
                        ExerciseHistoryStat = new ExerciseHistoryStatsRequest
                        {
                            Obciazenie = 10,
                            Powtorzenia = 5,
                            Serie = 6,
                            Czas = 0,
                            Dystans = 0
                        }
                    }
                }
            };
            return request;
        }
    }
}