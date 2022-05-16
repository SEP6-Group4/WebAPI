using WebAPI.Models;

namespace WebAPI.Data.Movies
{
    public interface IMovieService
    {
        Task<Movie> GetMovieByID(int id);
        Task<MovieList> GetMoviesByRatingDesc(int page);
        Task<MovieList> GetMoviesByRatingAsc(int page);
        Task<MovieList> GetMoviesByTitleAsc(int page);
        Task<MovieList> GetMoviesByTitleDesc(int page);
        Task<Credit> GetCreditsByMovieId(int movieId);
        Task<MovieList> GetMoviesBySearch(int page, string query);
    }
}
