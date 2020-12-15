using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FitnessTracker.Contracts;
using FitnessTracker.Contracts.Request.Training;
using FitnessTracker.Contracts.Response.Errors;
using FitnessTracker.Contracts.Response.Training;
using FitnessTracker.Helpers;
using FitnessTracker.Models;
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
    public class TrainingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITrainingService _trainingService;
        private readonly IAuthHelper _authHelper;

        public TrainingController(ITrainingService trainingService, IMapper mapper, IAuthHelper authHelper)
        {
            _trainingService = trainingService;
            _mapper = mapper;
            _authHelper = authHelper;
        }
        
        /// <summary>
        /// Zwraca listę treningów przypisanych do użytkownika
        /// </summary>
        /// <response code="200"></response>
        [SwaggerResponse(200, "", typeof(List<TrainingMinifiedResponse>))]
        //
        [HttpGet(ApiRoutes.Training.GetAllUserTrainings)]
        public async Task<IActionResult> GetAllUserTrainings()
        {
            return Ok(await _trainingService.GetAllUserTrainingsAsync());
        }
        
        /// <summary>
        /// Zwraca listę publicznych treningów, które nie są przypisane do użytkownika
        /// </summary>
        /// <response code="200"></response>
        [SwaggerResponse(200, "", typeof(List<PublicTrainingResponse>))]
        //
        [HttpGet(ApiRoutes.Training.GetAllAvailableUserPublicTrainings)]
        public async Task<IActionResult> GetAllAvailableUserPublicTrainings()
        {
            return Ok(await _trainingService.GetAllAvailableUserPublicTrainingsAsync());
        }
        
        /// <summary>
        /// Zwraca listę wszytkich publicznych treningów
        /// </summary>
        /// <response code="200"></response>
        [SwaggerResponse(200, "", typeof(List<PublicTrainingResponse>))]
        //
        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet(ApiRoutes.Training.GetAllPublicTrainings)]
        public async Task<IActionResult> GetAllPublicTrainings()
        {
            var trainingList = await _trainingService.GetAllPublicTrainingsAsync();
            return Ok(_mapper.Map<List<PublicTrainingResponse>>(trainingList));
        }
        
        /// <summary>
        /// Zwraca informacje o prywatnym treningu użytkownika lub o dowlonym publicznym
        /// </summary>
        /// <param name="trainingId"></param>
        /// <response code="200"></response>
        /// <response code="404"></response>
        [SwaggerResponse(200, "", typeof(TrainingFullResponse))]
        [SwaggerResponse(404)]
        //
        [HttpGet(ApiRoutes.Training.Get, Name = "GetTrainingById")]
        public async Task<IActionResult> GetTrainingById(int trainingId)
        {
            var training = await _trainingService.GetFullTrainingByIdAsync(trainingId);
            if (training == null)
                return NotFound();
            
            return Ok(training);
        }
        
        /// <summary>
        /// Tworzy nowy prywatny trening i przypisuje go do użytkownika
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201"></response>
        /// <response code="400"></response>
        [SwaggerResponse(201, "", typeof(TrainingMinifiedResponse))]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [HttpPost(ApiRoutes.Training.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTrainingRequest request)
        {
            var newTraining = _mapper.Map<Training>(request);
            var createdTraining = await _trainingService.CreateTrainingAsync(newTraining);
            
            if (createdTraining.Id == 0)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas dodawania"));
            
            return CreatedAtAction(nameof(GetTrainingById), 
                new {trainingId = createdTraining.Id},
                _mapper.Map<TrainingMinifiedResponse>(createdTraining));
        }    
        
        /// <summary>
        /// Tworzy nowy publiczny trening
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201"></response>
        /// <response code="400"></response>
        [SwaggerResponse(201, "", typeof(TrainingMinifiedResponse))]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost(ApiRoutes.Training.CreatePublic)]
        public async Task<IActionResult> CreatePublic([FromBody] CreateTrainingRequest request)
        {
            var newTraining = _mapper.Map<Training>(request);
            var createdTraining = await _trainingService.CreatePublicTrainingAsync(newTraining);
            
            if (createdTraining.Id == 0)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas dodawania"));
            
            return CreatedAtAction(nameof(GetTrainingById), 
                new {trainingId = createdTraining.Id},
                _mapper.Map<TrainingMinifiedResponse>(createdTraining));
        }
        
        /// <summary>
        /// Dodaje trening przypisany do użytkownika do ulubionych
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [HttpPatch(ApiRoutes.Training.ToggleTrainingFavourite)]
        public async Task<IActionResult> ToggleTrainingFavourite([FromBody] AddTrainingToFavouritesRequest request)
        {
            var training = await _trainingService.GetTrainingByIdAsync(request.TrainingId);

            if (training == null)
                return NotFound();

            var added = await _trainingService.ToggleTrainingFavouriteAsync(training, request.Favourite);
            
            if (!added)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas zapisywania"));

            return Ok();
        }

        /// <summary>
        /// Dodaje publiczny trening do listy treningów użytkownika
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [HttpPatch(ApiRoutes.Training.AssignPublicTrainingToUser)]
        public async Task<IActionResult> AddPublicTrainingToUser([FromBody] AddPublicTrainingToUserRequest request)
        {
            var training = await _trainingService.GetPublicTrainingByIdAsync(request.TrainingId);

            if (training == null)
                return NotFound();

            var added = await _trainingService.AddPublicTrainingToUser(training);
            
            if (!added)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas dodawania treningu"));

            return Ok();
        }

        /// <summary>
        /// Aktualizuje prywatny trening użytkownika
        /// </summary>
        /// <param name="trainingId"></param>
        /// <param name="request"></param>
        /// <response code="200"></response>
        /// <response code="403"></response>
        /// <response code="404"></response>
        [SwaggerResponse(200)]
        [SwaggerResponse(403, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [HttpPut(ApiRoutes.Training.Update)]
        public async Task<IActionResult> Update(
            [FromRoute] int trainingId, [FromBody] UpdateTrainingRequest request
        )
        {
            var training = await _trainingService.GetTrainingByIdAsync(trainingId);

            if (training == null)
                return NotFound();

            if (training.IsPublic)
                return StatusCode(403, new ErrorResponse("Nie możesz edytować publicznego treningu"));
            
            var updatedExercise = _mapper.Map(request, training);
            var updated = await _trainingService.UpdateTrainingAsync(updatedExercise);
            
            if (!updated)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas edytowania"));

            return Ok();
        }

        /// <summary>
        /// Aktualizuje publiczny trening
        /// </summary>
        /// <param name="trainingId"></param>
        /// <param name="request"></param>
        /// <response code="200"></response>
        /// <response code="404"></response>
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        //
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPut(ApiRoutes.Training.UpdatePublic)]
        public async Task<IActionResult> UpdatePublic(
            [FromRoute] int trainingId, [FromBody] UpdateTrainingRequest request
        )
        {
            var training = await _trainingService.GetPublicTrainingByIdAsync(trainingId);

            if (training == null)
                return NotFound();

            var updatedExercise = _mapper.Map(request, training);
            var updated = await _trainingService.UpdateTrainingAsync(updatedExercise);
            
            if (!updated)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas edytowania"));

            return Ok();
        }

        /// <summary>
        /// Usuwa prywatny lub publiczny trening przypisany do użytkownika
        /// </summary>
        /// <response code="204"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [SwaggerResponse(204)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [HttpDelete(ApiRoutes.Training.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int trainingId)
        {
            var training = await _trainingService.GetTrainingByIdAsync(trainingId);

            if (training == null)
                return NotFound();

            var deleted = await _trainingService.DeleteTrainingAsync(training);
            
            if (!deleted) 
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas usuwania"));

            return NoContent();
        }
        
        /// <summary>
        /// Usuwa publiczny trening
        /// </summary>
        /// <response code="204"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [SwaggerResponse(204)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [Authorize(Roles = "Admin,Moderator")]
        [HttpDelete(ApiRoutes.Training.DeletePublic)]
        public async Task<IActionResult> DeletePublicTraining([FromRoute] int trainingId)
        {
            var training = await _trainingService.GetPublicTrainingByIdAsync(trainingId);

            if (training == null)
                return NotFound();
            
            var deleted = await _trainingService.DeletePublicTrainingAsync(training);
            
            if (!deleted) 
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas usuwania"));

            return NoContent();
        }

        /// <summary>
        /// Przypisuje listę ćwiczeń do prywatnego treningu użytkownika
        /// </summary>
        /// <param name="trainingId"></param>
        /// <param name="request"></param>
        /// <response code="200"></response>
        /// <response code="403"></response>
        /// <response code="404"></response>
        /// <response code="400"></response>
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        [SwaggerResponse(403, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [HttpPatch(ApiRoutes.Training.AddExercisesToTraining)]
        public async Task<IActionResult> AddExercisesToTraining(
            [FromRoute] int trainingId, [FromBody] UpdateTrainingExercisesRequest request
        )
        {
            var training = await _trainingService.GetTrainingByIdAsync(trainingId);
            
            if (training == null)
                return NotFound();
            
            if (training.IsPublic)
                return StatusCode(403, new ErrorResponse("Nie masz uprawnień do edytowania tego treningu"));

            var updated = await _trainingService.UpdateTrainingExercisesAsync(training, request.Exercises);
            if (!updated) 
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas dodawania ćwiczeń do treningu"));

            return Ok();
        }
        
        /// <summary>
        /// Przypisuję liste ćwiczeń do publicznego treningu
        /// </summary>
        /// <param name="trainingId"></param>
        /// <param name="request"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPatch(ApiRoutes.Training.AddExercisesToPublicTraining)]
        public async Task<IActionResult> AddExercisesToPublicTraining(
            [FromRoute] int trainingId, [FromBody] UpdateTrainingExercisesRequest request
        )
        {
            var training = await _trainingService.GetPublicTrainingByIdAsync(trainingId);
            
            if (training == null)
                return NotFound();

            var updated = await _trainingService.UpdateTrainingExercisesAsync(training, request.Exercises);
            if (!updated) 
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas dodawania ćwiczeń do treningu"));

            return Ok();
        }

        /// <summary>
        /// Dodaje trening użytkownika i ćwiczenia do historii
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [HttpPatch(ApiRoutes.Training.AddTrainingToHistory)]
        public async Task<IActionResult> AddTrainingToHistory([FromBody] AddTrainingToHistoryRequest request)
        {
            var training = await _trainingService.GetTrainingByIdAsync(request.TrainingId);
            
            if (training == null)
                return NotFound();
        
            List<ExerciseHistory> histories = new List<ExerciseHistory>();
            foreach (ExerciseHistoryRequest exercise in request.Exercises)
            {
                histories.Add(new ExerciseHistory
                {
                    ExerciseId = exercise.ExerciseId,
                    ExerciseHistoryStats = new List<ExerciseHistoryStats>
                    {
                        _mapper.Map<ExerciseHistoryStats>(exercise.ExerciseHistoryStat)
                    }
                });
            }
            
            var updated = await _trainingService.AddTrainingToHistory(training, histories);

            if (!updated) 
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas dodawania treningu do historii"));

            return Ok();
        }

    }
}