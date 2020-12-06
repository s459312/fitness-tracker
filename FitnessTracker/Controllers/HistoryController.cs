using AutoMapper;
using FitnessTracker.Contracts;
using FitnessTracker.Contracts.Request.Coach;
using FitnessTracker.Data;
using FitnessTracker.Helpers;
using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace FitnessTracker.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class HistoryController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IAuthHelper _authHelper;
        private readonly DatabaseContext _context;

        public HistoryController(IUserService userService, IMapper mapper, IUriService uriService, IAuthHelper authHelper, DatabaseContext context)
        {
            _userService = userService;
            _mapper = mapper;
            _uriService = uriService;
            _authHelper = authHelper;
            _context = context;
        }

        [HttpGet(ApiRoutes.History.Index)]
        public IActionResult Index() => Ok(from his in _context.TrainingHistory orderby his.Date select new { his.Date, his.UserId, his.Training });

        [HttpGet(ApiRoutes.History.Exercise)]
        public async Task<IActionResult> GetCoach(int exerciseId)
        {

            var ret = await _context.Exercise.FindAsync(exerciseId);
            return ret != null ? (IActionResult) Ok(from his in _context.ExerciseHistory.AsEnumerable() where his.Exercise.Equals(ret) group his by his.Date into g select g) : NotFound();

        }

    }

}
