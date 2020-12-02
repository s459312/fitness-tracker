using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FitnessTracker.Contracts;
using FitnessTracker.Contracts.Request.Queries;
using FitnessTracker.Contracts.Request.User;
using FitnessTracker.Contracts.Response;
using FitnessTracker.Contracts.Response.Errors;
using FitnessTracker.Contracts.Response.User;
using FitnessTracker.Helpers;
using FitnessTracker.Models;
using FitnessTracker.Models.Filters;
using FitnessTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitnessTracker.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IAuthHelper _authHelper;

        public UserController(IUserService userService, IMapper mapper, IUriService uriService, IAuthHelper authHelper)
        {
            _userService = userService;
            _mapper = mapper;
            _uriService = uriService;
            _authHelper = authHelper;
        }
        
        /// <summary>
        /// Zwraca listę wszystkich użytkowników
        /// </summary>
        /// <param name="paginationQuery"></param>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200, "", typeof(PagedResponse<List<UserResponse>>))]
        //
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]       
        [HttpGet(ApiRoutes.User.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery)
        {
            paginationQuery = PaginationHelper.ValidateQuery(paginationQuery);
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);

            var users = await _userService.GetAllUsersAsync(paginationFilter);
            var usersCount = await _userService.CountUsersAsync();

            var userResponses = _mapper.Map<List<UserResponse>>(users);
            var paginatedResponse =
                PaginationHelper.Paginate(_uriService, paginationFilter, userResponses, usersCount);
            
            return Ok(paginatedResponse);
        }
        
        /// <summary>
        /// Zwraca zalogowanego użytkownika
        /// </summary>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200, "", typeof(UserResponse))]
        //
        [HttpGet(ApiRoutes.User.Get)]
        public async Task<IActionResult> GetById()
        {
            var user = await _userService.GetUserByIdAsync(_authHelper.GetAuthenticatedUserId());
            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<UserResponse>(user));
        }
        
        /// <summary>
        /// Aktualizuje informacje zalogowanego użytkownika
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [HttpPut(ApiRoutes.User.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            var user = await _userService.GetUserByIdAsync(_authHelper.GetAuthenticatedUserId());
            if (user == null)
                return NotFound();
            
            User updatedUser = _mapper.Map(request, user);
            bool successfullyUpdated = await _userService.UpdateUserAsync(updatedUser);

            if (!successfullyUpdated)
                return BadRequest(new ErrorResponse("There was a problem during update"));

            return Ok();
        }

        /// <summary>
        /// Zmienia role użytkownika o podanym id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPatch(ApiRoutes.User.UpdateRole)]
        public async Task<IActionResult> UpdateRole([FromRoute] int userId, [FromBody] UpdateUserRoleRequest request)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound();
            
            User updatedUser = _mapper.Map(request, user);
            bool successfullyUpdated = await _userService.UpdateUserAsync(updatedUser);

            if (!successfullyUpdated)
                return BadRequest(new ErrorResponse("There was a problem during role update"));

            return Ok();
        }
        
        /// <summary>
        /// Usuwa użytkownika o podanym id
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="204"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(204)]
        [SwaggerResponse(404)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete(ApiRoutes.User.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound();
            
            bool successfullyDeleted = await _userService.DeleteUserAsync(user);
            if (!successfullyDeleted)
                return BadRequest(new ErrorResponse("Failed to delete user"));

            return NoContent();
        }
    }
}