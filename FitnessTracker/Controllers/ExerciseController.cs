using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FitnessTracker.Contracts;
using FitnessTracker.Contracts.Request.Exercise;
using FitnessTracker.Contracts.Request.Queries;
using FitnessTracker.Contracts.Response;
using FitnessTracker.Contracts.Response.Errors;
using FitnessTracker.Contracts.Response.Exercise;
using FitnessTracker.Data;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExerciseController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IExerciseService _exerciseService;
        private readonly IAuthHelper _authHelper;
        private readonly DatabaseContext _context;

        public ExerciseController(IMapper mapper, IUriService uriService, IExerciseService exerciseService, IAuthHelper authHelper, DatabaseContext context)
        {
            _mapper = mapper;
            _uriService = uriService;
            _exerciseService = exerciseService;
            _authHelper = authHelper;
            _context = context;
        }

        /// <summary>
        /// Zwraca listę wszystkich ćwiczeń
        /// </summary>
        /// <param name="paginationQuery"></param>
        /// <param name="exerciseQuery"></param>
        /// <response code="200"></response>
        [SwaggerResponse(200, "", typeof(PagedResponse<List<ExerciseResponse>>))]
        //
        [HttpGet(ApiRoutes.Exercise.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] ExerciseQuery exerciseQuery)
        {
            paginationQuery = PaginationHelper.ValidateQuery(paginationQuery);
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var exerciseFilter = _mapper.Map<ExerciseFilter>(exerciseQuery);
            
            var exerciseList = await _exerciseService.GetAllExercisesAsync(paginationFilter, exerciseFilter);
            var exerciseCount = await _exerciseService.ExercisesCountAsync(exerciseFilter);
            
            var exerciseResponse = _mapper.Map<List<ExerciseResponse>>(exerciseList);
            var paginatedResponse =
                PaginationHelper.Paginate(_uriService, paginationFilter, exerciseResponse, exerciseCount);
            
            return Ok(paginatedResponse);
        }

        /// <summary>
        /// Zwraca tablicę Id wszystkich ćwiczeń użytkownika
        /// </summary>
        /// <response code="200"></response>
        [SwaggerResponse(200, "", typeof(int[]))]
        [HttpGet(ApiRoutes.Exercise.GetMine)]
        public async Task<IActionResult> GetMine()
        {
            return Ok((from obj in (await _authHelper.GetAuthenticatedUserModel(_context)).ExerciseHistories select obj.Exercise.Id).Distinct());
        }

        /// <summary>
        /// Zwraca pojedyńcze ćwiczenie
        /// </summary>
        /// <param name="exerciseId"></param>
        /// <response code="200"></response>
        /// <response code="404"></response>
        [SwaggerResponse(200, "", typeof(ExerciseResponse))]
        [SwaggerResponse(404)]
        //
        [HttpGet(ApiRoutes.Exercise.Get, Name = "GetById")]
        public async Task<IActionResult> GetById([FromRoute] int exerciseId)
        {
            var exercise = await _exerciseService.GetExerciseByIdAsync(exerciseId);
            if (exercise == null)
                return NotFound();

            return Ok(_mapper.Map<ExerciseResponse>(exercise));
        }
        
        /// <summary>
        /// Tworzy nowe ćwiczenie
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201"></response>
        /// <response code="400"></response>
        [SwaggerResponse(201, "", typeof(ExerciseResponse))]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost(ApiRoutes.Exercise.Create)]
        public async Task<IActionResult> Create([FromBody] CreateExerciseRequest request)
        {
            var newExercise = _mapper.Map<Exercise>(request);
            var createdExercise = await _exerciseService.CreateExerciseAsync(newExercise);
            
            if (createdExercise.Id == 0)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas dodawania"));
            
            return CreatedAtAction(nameof(GetById), 
                new {exerciseId = createdExercise.Id},
                _mapper.Map<ExerciseResponse>(createdExercise));
        }

        /// <summary>
        /// Aktualizuje istniejące ćwiczenie
        /// </summary>
        /// <param name="request"></param>
        /// <param name="exerciseId"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPut(ApiRoutes.Exercise.Update)]
        public async Task<IActionResult> Update([FromRoute] int exerciseId, [FromBody] UpdateExerciseRequest request)
        {
            var exercise = await _exerciseService.GetExerciseByIdAsync(exerciseId);

            if (exercise == null)
                return NotFound();
            
            var updatedExercise = _mapper.Map(request, exercise);
            var updated = await _exerciseService.UpdateExerciseAsync(updatedExercise);
            
            if (!updated)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas edytowania"));

            return Ok();
        }

        /// <summary>
        /// Usuwa ćwiczenie
        /// </summary>
        /// <param name="exerciseId"></param>
        /// <response code="204"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [SwaggerResponse(204)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [Authorize(Roles = "Admin,Moderator")]
        [HttpDelete(ApiRoutes.Exercise.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int exerciseId)
        {
            var exercise = await _exerciseService.GetExerciseByIdAsync(exerciseId);
            
            if (exercise == null)
                return NotFound();

            var deleted = await _exerciseService.DeleteExerciseAsync(exercise);
            
            if (!deleted)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas usuwania"));

            return NoContent();
        }
    }
}