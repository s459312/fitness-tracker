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

namespace FitnessTracker.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CoachController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IAuthHelper _authHelper;
        private readonly DatabaseContext _context;

        public CoachController(IUserService userService, IMapper mapper, IUriService uriService, IAuthHelper authHelper, DatabaseContext context)
        {
            _userService = userService;
            _mapper = mapper;
            _uriService = uriService;
            _authHelper = authHelper;
            _context = context;
        }

        [HttpGet(ApiRoutes.Coach.Index)]
        public IActionResult Index() => Ok(_context.Coach);

        [HttpGet(ApiRoutes.Coach.GetCoach)]
        public async Task<IActionResult> GetCoach(int coachId)
        {

            var ret = await _context.Coach.FindAsync(coachId);
            return ret != null ? (IActionResult) Ok(ret) : NotFound();

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Moderator")]
        [HttpPost(ApiRoutes.Coach.PostCoach)]
        public async Task<IActionResult> AddCoach([FromBody] CreateCoach request)
        {

            var coach = _mapper.Map<Coach>(request);

            await _context.Coach.AddAsync(coach);
            
            try
            {

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCoach),
                        new { coachId = coach.Id },
                        coach); //TODO: Response zamiast AddCoach (dodać Mappera do Respone i nie zwracać Goal)

            }
            catch
            {

                return UnprocessableEntity();

            }

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Moderator")]
        [HttpPut(ApiRoutes.Coach.PutCoach)]
        public async Task<IActionResult> EditCoach([FromRoute] int coachId, [FromBody] CreateCoach request)
        {

            var coach = await _context.Coach.FindAsync(coachId);
            if (coach == null)
                return NotFound();

            try
            {

                Coach updatedCoach = _mapper.Map(request, coach);
                await _context.SaveChangesAsync();

                return Ok(updatedCoach);

            }
            catch
            {

                return UnprocessableEntity();

            }

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Moderator")]
        [HttpDelete(ApiRoutes.Coach.DeleteCoach)]
        public async Task<IActionResult> DeleteCoach(int coachId)
        {

            var coach = await _context.Coach.FindAsync(coachId);
            if (coach == null) return NotFound();

            _context.Coach.Remove(coach);
            await _context.SaveChangesAsync();
            return NoContent();

        }

    }

}
