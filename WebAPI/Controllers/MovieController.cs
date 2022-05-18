using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Movies;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Movie))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Movie>> GetMovieByID(int id)
        {
            try
            {
                Movie movie = await movieService.GetMovieByID(id);
                return Ok(movie);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("ByRatingDesc")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieList))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieList>> GetMoviesByRatingDesc(int page)
        {
            try
            {
                MovieList movies = await movieService.GetMoviesByRatingDesc(page);
                return Ok(movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("ByRatingAsc")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieList))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieList>> GetMoviesByRatingAsc(int page)
        {
            try
            {
                MovieList movies = await movieService.GetMoviesByRatingAsc(page);
                return Ok(movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("ByTitleAsc")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieList))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieList>> GetMoviesByTitleAsc(int page)
        {
            try
            {
                MovieList movies = await movieService.GetMoviesByTitleAsc(page);
                return Ok(movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("ByTitleDesc")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieList))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieList>> GetMoviesByTitleDesc(int page)
        {
            try
            {
                MovieList movies = await movieService.GetMoviesByTitleDesc(page);
                return Ok(movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("{id}/credits")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Credit))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Credit>> GetCredits(int id)
        {
            try
            {
                Credit credit = await movieService.GetCreditsByMovieId(id);
                return Ok(credit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("search/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieList))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieList>> SearchForMovies([FromQuery] int page, [FromQuery] string query)
        {
            try
            {
                MovieList movies = await movieService.GetMoviesBySearch(page, query);
                return Ok(movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
