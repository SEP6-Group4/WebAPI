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

        [HttpGet("getFavoritesByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<MovieList> GetFavoriteMoviesByEmail([FromQuery] string email)
        {
            return await favoriteMovieService.GetFavoriteMoviesByEmail(email);
        }

        [HttpGet("getFavorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<bool> GetIsFavoriteMovieByID([FromQuery] int userID, [FromQuery] int movieID)
        {
            return await favoriteMovieService.GetIsFavoriteMovieByID(userID, movieID);
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
