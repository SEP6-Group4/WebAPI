using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.FavouriteActor;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FavouriteActorController : ControllerBase
    {
        private IFavouriteActorService service;

        public FavouriteActorController(IFavouriteActorService service)
        {
            this.service = service;
        }

        [HttpPost("AddActorToFavourite/{userId}/{actorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddActorToFavourite(int userId, int actorId)
        {
            await service.AddActorToFavourite(userId, actorId);
            return Ok();
        }

        [HttpDelete("RemoveActorFromFavourite/{userId}/{actorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveActorFromFavourite(int userId, int actorId)
        {
            await service.RemoveActorFromFavourite(userId, actorId);
            return Ok();
        }

        [HttpGet("GetFavouriteActorIds/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<int>> GetFavouriteActorIds(int userId)
        {
            return await service.GetFavouriteActorIds(userId);
            
        }

        [HttpGet("GetFavouriteActorIdsByEmail/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<Actor>> GetFavouriteActorIdsByEmail(string email)
        {
            return await service.GetFavouriteActorIdsByEmail(email);

        }
    }
}
