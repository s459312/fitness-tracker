using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FitnessTracker.Contracts;
using FitnessTracker.Contracts.Response.Goal;
using FitnessTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitnessTracker.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class GoalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGoalService _goalService;

        public GoalController(IMapper mapper, IGoalService goalService)
        {
            _mapper = mapper;
            _goalService = goalService;
        }
        
        /// <summary>
        /// Zwraca wszystkie cele
        /// </summary>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200, "", typeof(List<GoalResponse>))]
        //
        [HttpGet(ApiRoutes.Goal.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var goals = await _goalService.GetAllGoalsAsync();
            return Ok(_mapper.Map<List<GoalResponse>>(goals));
        }
    }
}