using AutoMapper;
using FitnessTracker.Contracts;
using FitnessTracker.Data;
using FitnessTracker.Helpers;
using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HistoryController : ControllerBase
    {
        /*
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;*/
        private readonly IAuthHelper _authHelper;
        private readonly DatabaseContext _context;

        public HistoryController(/*IUserService userService, IMapper mapper, IUriService uriService, */IAuthHelper authHelper, DatabaseContext context)
        {/*
            _userService = userService;
            _mapper = mapper;
            _uriService = uriService;*/
            _authHelper = authHelper;
            _context = context;
        }

        [HttpGet(ApiRoutes.History.Index)]
        public IActionResult Index() => 
            Ok(from his in _context.TrainingHistory.Where(x => x.UserId == _authHelper.GetAuthenticatedUserId()) orderby his.Date select new { his.Date, his.Training.Id, his.Training.Name });

        [HttpGet(ApiRoutes.History.Exercise)]
        public async Task<IActionResult> GetExercise(int exerciseId)
        {

            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _authHelper.GetAuthenticatedUserId());
            if (user == null) return NotFound();

            var exerciseStatsResult =
               from history in user.ExerciseHistories
               join historyStats in _context.ExerciseHistoryStats on history.Id equals historyStats.ExerciseHistoryId
               join exercise in _context.Exercise on history.ExerciseId equals exercise.Id
               where exercise.Id == exerciseId
               group historyStats by history.Date into g
               select new
               {
                   Date = g.Key,
                   Powtorzenia = g.Sum(x => x.Powtorzenia),
                   Serie = g.Sum(x => x.Serie),
                   Obciazenie = g.Sum(x => x.Obciazenie),
                   Czas = g.Sum(x => x.Czas),
                   Dystans = g.Sum(x => x.Dystans)
               };

            return Ok(exerciseStatsResult);

        }

    }

}
