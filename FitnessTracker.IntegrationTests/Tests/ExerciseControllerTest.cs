using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using FitnessTracker.Contracts;
using FitnessTracker.Contracts.Request.Exercise;
using FitnessTracker.Contracts.Response;
using FitnessTracker.Contracts.Response.Exercise;
using FitnessTracker.IntegrationTests.Factories;
using FitnessTracker.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FitnessTracker.IntegrationTests.Tests
{
    public class ExerciseControllerTest : IntegrationTestCore
    {
        [Fact]
        public async Task Controller__Unauthenticated_User_Cant_Access_Endpoints()
        {
            foreach (PropertyInfo propertyInfo in typeof(ApiRoutes.Exercise).GetProperties())
            {
                var response = await Client.GetAsync(propertyInfo.GetValue(null).ToString());
                response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            }
        }
        
        [Fact]
        public async Task GetAll__Authenticated_User_Can_Retrieve_All_Exercises()
        {
            await LogInAs("user@gmail.com");
            await CreateExercise();
            await CreateExercise();

            var response = await Client.GetAsync(ApiRoutes.Exercise.GetAll);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var body = await response.Content.ReadAsAsync<PagedResponse<ExerciseResponse>>();
            body.Data.Should().NotBeEmpty();
            body.Data.Should().HaveCountGreaterOrEqualTo(2);
            
            body.Meta.Should().NotBeNull();
            body.Meta.PageNumber.Should().Be(1);
            body.Meta.Total.Should().Be(2);
            body.Meta.TotalPages.Should().Be(1);
        }
        
        [Fact]
        public async Task Get__Authenticated_User_Can_Retrieve_One_Example()
        {
            await LogInAs("user@gmail.com");
            Exercise createdExercise = await CreateExercise();

            var getEndpoint = ApiRoutes.Exercise.Get.Replace("{exerciseId}", createdExercise.Id.ToString());
            var response = await Client.GetAsync(getEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var responseData = await response.Content.ReadAsAsync<ExerciseResponse>();
            
            responseData.Id.Should().Be(createdExercise.Id);
            responseData.Name.Should().Be(createdExercise.Name);
            responseData.Description.Should().Be(createdExercise.Description);
            responseData.Powtorzenia.Should().Be(createdExercise.Powtorzenia);
            responseData.Serie.Should().Be(createdExercise.Serie);
            responseData.Obciazenie.Should().Be(createdExercise.Obciazenie);
            responseData.Czas.Should().Be(createdExercise.Czas);
            responseData.Dystans.Should().Be(createdExercise.Dystans);
        }

        [Fact]
        public async Task Create__Admin_Or_Moderator_Can_Create_Exercise()
        {
            await LogInAs("admin@gmail.com");
            CreateExerciseRequest exerciseRequest = Factory.Exercise.CreateExerciseRequest();
            
            var response = await Client.PostAsJsonAsync(ApiRoutes.Exercise.Create, exerciseRequest);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            
            var responseData = await response.Content.ReadAsAsync<ExerciseResponse>();
            responseData.Id.Should().Be(1);
            responseData.Name.Should().Be(exerciseRequest.Name);
            responseData.Description.Should().Be(exerciseRequest.Description);
            responseData.Powtorzenia.Should().Be(exerciseRequest.Powtorzenia);
            responseData.Serie.Should().Be(exerciseRequest.Serie);
            responseData.Obciazenie.Should().Be(exerciseRequest.Obciazenie);
            responseData.Czas.Should().Be(exerciseRequest.Czas);
            responseData.Dystans.Should().Be(exerciseRequest.Dystans);
            
            int count = await Context.Exercise.AsNoTracking().CountAsync();
            count.Should().Be(1);
            
            await LogInAs("moderator@gmail.com");
            response = await Client.PostAsJsonAsync(ApiRoutes.Exercise.Create, exerciseRequest);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Create__Normal_User_Can_Not_Create_Exercise()
        {
            await LogInAs("user@gmail.com");
            CreateExerciseRequest exerciseRequest = Factory.Exercise.CreateExerciseRequest();
            
            var response = await Client.PostAsJsonAsync(ApiRoutes.Exercise.Create, exerciseRequest);
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        
        [Fact]
        public async Task Update__Admin_Or_Moderator_Can_Update_Exercise()
        {
            await LogInAs("admin@gmail.com");
            Exercise createdExercise = await CreateExercise();
            UpdateExerciseRequest exerciseRequest = Factory.Exercise.UpdateExerciseRequest();
            string updateEndpoint = ApiRoutes.Exercise.Update.Replace("{exerciseId}", createdExercise.Id.ToString());
            
            var response = await Client.PutAsJsonAsync(updateEndpoint, exerciseRequest);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var updatedExample = await Context.Exercise
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == createdExercise.Id);
            
            updatedExample.Id.Should().Be(createdExercise.Id);
            updatedExample.Name.Should().Be(exerciseRequest.Name);
            updatedExample.Description.Should().Be(exerciseRequest.Description);
            updatedExample.Powtorzenia.Should().Be(exerciseRequest.Powtorzenia);
            updatedExample.Serie.Should().Be(exerciseRequest.Serie);
            updatedExample.Obciazenie.Should().Be(exerciseRequest.Obciazenie);
            updatedExample.Czas.Should().Be(exerciseRequest.Czas);
            updatedExample.Dystans.Should().Be(exerciseRequest.Dystans);
            
            int count = await Context.Exercise.AsNoTracking().CountAsync();
            count.Should().Be(1);
            
            await LogInAs("moderator@gmail.com");
            response = await Client.PutAsJsonAsync(updateEndpoint, exerciseRequest);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Update__Normal_User_Can_Not_Update_Exercise()
        {
            await LogInAs("user@gmail.com");
            Exercise createdExercise = await CreateExercise();
            UpdateExerciseRequest exerciseRequest = Factory.Exercise.UpdateExerciseRequest();
            string updateEndpoint = ApiRoutes.Exercise.Update.Replace("{exerciseId}", createdExercise.Id.ToString());
            
            var response = await Client.PutAsJsonAsync(updateEndpoint, exerciseRequest);
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Delete__Admin_Or_Moderator_Can_Delete_Exercise()
        {
            await LogInAs("admin@gmail.com");
            Exercise createdExercise = await CreateExercise();
            string deleteEndpoint = ApiRoutes.Exercise.Delete.Replace("{exerciseId}", createdExercise.Id.ToString());
            
            var response = await Client.DeleteAsync(deleteEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            int count = await Context.Exercise.AsNoTracking().CountAsync();
            count.Should().Be(0);
        }
        
        [Fact]
        public async Task Delete__Normal_User_Can_Not_Delete_Exercise()
        {
            await LogInAs("user@gmail.com");
            Exercise createdExercise = await CreateExercise();
            string deleteEndpoint = ApiRoutes.Exercise.Delete.Replace("{exerciseId}", createdExercise.Id.ToString());
            
            var response = await Client.DeleteAsync(deleteEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            int count = await Context.Exercise.AsNoTracking().CountAsync();
            count.Should().Be(1);
        }
        
        private async Task<Exercise> CreateExercise()
        {
            Exercise createdExercise = Factory.Exercise.GetModel();
            await Context.Exercise.AddAsync(createdExercise);
            await Context.SaveChangesAsync();
            return createdExercise;
        }
    }
}