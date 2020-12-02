using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FitnessTracker.Contracts;
using FitnessTracker.Contracts.Request.Auth;
using FitnessTracker.Contracts.Response.Auth;
using FitnessTracker.Data;
using FitnessTracker.IntegrationTests.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace FitnessTracker.IntegrationTests
{
    public class IntegrationTestCore : IDisposable
    {
        
        protected readonly HttpClient Client;
        protected readonly DatabaseContext Context;
        protected readonly IServiceProvider ServiceProvider;
        
        public IntegrationTestCore()
        {
            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                 typeof(DbContextOptions<DatabaseContext>));
                        services.Remove(descriptor);
                        services.AddDbContext<DatabaseContext>(options =>
                        {
                            options.UseInMemoryDatabase("testDB");
                            //options.UseSqlite("Filename=TestDatabase.db");
                        });
                        services.BuildServiceProvider();
                    });
                });
            
            ServiceProvider = factory.Services;
            Context = ServiceProvider.GetService<DatabaseContext>();
            
            Context = ServiceProvider.GetService<DatabaseContext>();
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            Client = factory.CreateClient();
        }

        protected async Task<AuthenticatedUser> LogInAs(string email = "admin@gmail.com")
        {
            IdentityModelEventSource.ShowPII = true;
            var tokenString = await GetJwtAsync(email);
            Client.DefaultRequestHeaders.Authorization = null;
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString);
            
            var id = token.Claims.FirstOrDefault(c => c.Type == "id");
            var role = token.Claims.FirstOrDefault(x => x.Type == "role");
            
            return new AuthenticatedUser
            {
                Id = id == null ? 0 : Int32.Parse(id.Value),
                Role = role == null ? String.Empty : role.Value
            };
        }

        protected void Logout()
        {
            Client.DefaultRequestHeaders.Authorization = null;
        }
        
        private async Task<string> GetJwtAsync(string email)
        {
            var response =
                await Client.PostAsJsonAsync(ApiRoutes.Auth.Login,
                    new AuthLoginRequest
                    {
                        Email = email,
                        Password = "Password#2!"
                    });
            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        public void Dispose()
        {
            Client?.Dispose();
            Context?.Dispose();
        }
    }
}