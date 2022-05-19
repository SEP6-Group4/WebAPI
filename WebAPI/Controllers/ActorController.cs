using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Actors;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        IActorService actorService;

        public ActorController(IActorService actorService)
        {
            this.actorService = actorService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Actor))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Actor>> GetActorByID(int id)
        {
            try
            {
                Actor actor = await actorService.GetActorByID(id);
                return Ok(actor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}/movie_credits")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieCredit))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieCredit>> GetMovieCredits(int id)
        {
            try
            {
                MovieCredit credit = await actorService.GetMovieCreditsByActorId(id);
                return Ok(credit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
