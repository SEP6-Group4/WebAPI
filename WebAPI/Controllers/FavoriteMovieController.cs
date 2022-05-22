using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Favorites;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteMovieController : ControllerBase
    {
        IFavoriteMovieService favoriteMovieService;

        public FavoriteMovieController(IFavoriteMovieService favoriteMovieService)
        {
            this.favoriteMovieService = favoriteMovieService;
        }

        [HttpPost("addFavorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task AddFavoriteMovie([FromQuery] int userID, [FromQuery] int movieID)
        {
            await favoriteMovieService.AddFavoriteMovie(userID, movieID);
        }

        [HttpGet("getFavorites")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<MovieList> GetFavoriteMoviesByID([FromQuery] int userID)
        {
            return await favoriteMovieService.GetFavoriteMoviesByID(userID);
        }

        [HttpGet("getFavoritesByUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<Movie>> GetFavoriteMoviesByUser([FromQuery] int userID)
        {
            return await favoriteMovieService.GetFavoriteMoviesByUser(userID);
        }

        [HttpGet("getFavorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<bool> GetIsFavoriteMovieByID([FromQuery] int userID, [FromQuery] int movieID)
        {
            return await favoriteMovieService.GetIsFavoriteMovieByID(userID, movieID);
        }

        [HttpGet("getFavoriteMovieCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetFavoriteMovieCount([FromQuery] int movieID)
        {
            int count =  await favoriteMovieService.GetFavoriteMovieCount(movieID);
            return  Ok(count);
        }

        [HttpGet("getFavoriteMovieIdsByAgeGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<IdCount>>> GetFavoriteMovieIdsByAgeGroup([FromQuery] int ageGroup)
        {
            List<IdCount> movieIDs = await favoriteMovieService.GetFavoriteMoviesByAgeGroup(ageGroup);
            return Ok(movieIDs);
        }

        [HttpGet("getFavoriteMovieIdsByAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<IdCount>>> GetFavoriteMovieIdsByAll()
        {
            List<IdCount> movieIDs = await favoriteMovieService.GetFavoriteMoviesByAll();
            return Ok(movieIDs);
        }

        [HttpDelete("removeFavorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task RemoveFavoriteMovieByID([FromQuery] int userID, [FromQuery] int movieID)
        {
            await favoriteMovieService.RemoveFavoriteMovieByID(userID, movieID);
        }
    }
}
