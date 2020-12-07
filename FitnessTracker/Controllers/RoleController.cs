using AutoMapper;
using FitnessTracker.Contracts;
using FitnessTracker.Contracts.Response.Role;
using FitnessTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public RoleController(IMapper mapper, IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        /// <summary>
        /// Zwraca wszystkie dostępne role
        /// </summary>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200, "", typeof(List<RoleResponse>))]
        //
        [HttpGet(ApiRoutes.Role.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(_mapper.Map<List<RoleResponse>>(roles));
        }
    }
}