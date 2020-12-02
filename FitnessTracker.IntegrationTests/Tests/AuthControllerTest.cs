using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using FitnessTracker.Contracts;
using FitnessTracker.Contracts.Request.Auth;
using FitnessTracker.Contracts.Response.Auth;
using FitnessTracker.Helpers;
using FitnessTracker.IntegrationTests.Factories;
using FitnessTracker.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FitnessTracker.IntegrationTests.Tests
{
    public class AuthControllerTest : IntegrationTestCore
    {
        // Register 
        [Fact]
        public async Task Register__Fields_Are_Required()
        {
            foreach (PropertyInfo propertyInfo in typeof(AuthRegisterRequest).GetProperties())
            {
                AuthRegisterRequest registerRequest = Factory.Auth.AuthRegisterRequest();
                propertyInfo.SetValue(registerRequest, "");
                
                var response = await Client.PostAsJsonAsync(ApiRoutes.Auth.Register, registerRequest);
                response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            }
        }
        
        [Fact]
        public async Task Register__User_Can_Create_Account()
        {
            var registerRequest = Factory.Auth.AuthRegisterRequest();
            var response = await Client.PostAsJsonAsync(ApiRoutes.Auth.Register, registerRequest);
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var registerResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();

            registerResponse.Token.Should().NotBeNullOrEmpty();
            registerResponse.RefreshToken.Should().NotBeNullOrEmpty();
            
            // TODO Validate token
            
            var newUser = Context.Users.FirstOrDefault(x => x.Email == registerRequest.Email.ToLower());
            newUser.Should().NotBeNull();
            newUser.Email.Should().Be(registerRequest.Email.ToLower());
            newUser.Name.Should().Be(registerRequest.Name);
            newUser.Surname.Should().Be(registerRequest.Surname);
        }

        [Fact]
        public async Task Register__User_Must_Have_Unique_Email()
        {
            var registerRequest = Factory.Auth.AuthRegisterRequest();
            
            var response = await Client.PostAsJsonAsync(ApiRoutes.Auth.Register, registerRequest);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            response = await Client.PostAsJsonAsync(ApiRoutes.Auth.Register, registerRequest);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            
        }

        // Login
        [Fact]
        public async Task Login__Fields_Are_Required()
        {
            foreach (PropertyInfo propertyInfo in typeof(AuthLoginRequest).GetProperties())
            {
                AuthLoginRequest loginRequest = Factory.Auth.AuthLoginRequest();
                propertyInfo.SetValue(loginRequest, "");
                
                var response = await Client.PostAsJsonAsync(ApiRoutes.Auth.Login, loginRequest);
                response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            }
        }
        
        [Fact]
        public async Task Login__Existing_User_Can_Login()
        {
            User user = Factory.UserFactory.GetModel();
            user.Email = user.Email.ToLower();
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();

            AuthLoginRequest loginRequest = Factory.Auth.AuthLoginRequest(user.Email);
        
            var response = await Client.PostAsJsonAsync(ApiRoutes.Auth.Login, loginRequest);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        
            var registerResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();

            registerResponse.Token.Should().NotBeNullOrEmpty();
            registerResponse.RefreshToken.Should().NotBeNullOrEmpty();
        }
        
        [Fact]
        public async Task Login__User_Token_Should_Have_Role_Claim()
        {
            User user = Factory.UserFactory.GetModel("Admin");
            user.Email = user.Email.ToLower();
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();

            AuthLoginRequest loginRequest = Factory.Auth.AuthLoginRequest(user.Email);
        
            var response = await Client.PostAsJsonAsync(ApiRoutes.Auth.Login, loginRequest);
            var loginResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(loginResponse.Token);
            
            var role = token.Claims.FirstOrDefault(x => x.Type == "role")?.Value;
            var dbUser = await Context.Users.FirstOrDefaultAsync(x => x.Email == loginRequest.Email.ToLower());
            var dbRole = await Context.Roles.FirstOrDefaultAsync(x => x.Name == role);

            dbRole.Should().NotBeNull();
            role.Should().Be(dbRole.Name);
            dbUser.RoleId.Should().Be(user.RoleId);
        }
        
        [Fact]
        public async Task Login__Non_Existing_User_Cant_Login()
        {
            User user = Factory.UserFactory.GetModel();
            user.Email = user.Email.ToLower();

            AuthLoginRequest loginRequest = Factory.Auth.AuthLoginRequest(user.Email);
        
            var response = await Client.PostAsJsonAsync(ApiRoutes.Auth.Login, loginRequest);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        // Change Password
        [Fact]
        public async Task ChangePassword__User_Can_Change_Password()
        {
            User user = Factory.UserFactory.GetModel();
            user.Email = user.Email.ToLower();

            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
                
            await LogInAs(user.Email);

            AuthChangePasswordRequest changePasswordRequest = Factory.Auth.AuthChangePasswordRequest();
            var response = await Client.PutAsJsonAsync(ApiRoutes.Auth.ChangePassword, changePasswordRequest);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // AsNoTracking - Refresh User context to get changes after update update in controller
            var updatedUser = await Context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == user.Email);
            var authHelper = new AuthHelper();

            updatedUser.PasswordHash.Should().NotEqual(user.PasswordHash);
            updatedUser.PasswordSalt.Should().NotEqual(user.PasswordSalt);
                
            bool valid = authHelper.VerifyPasswordHash(changePasswordRequest.NewPassword, updatedUser.PasswordHash, updatedUser.PasswordSalt);
            valid.Should().BeTrue();
        }
    }
}