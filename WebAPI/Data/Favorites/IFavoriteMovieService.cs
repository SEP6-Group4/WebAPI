using WebAPI.Models;

namespace WebAPI.Data.Favorites
{
    public interface IFavoriteMovieService
    {
        Task AddFavoriteMovie(int userID, int movieID);
        Task<MovieList> GetFavoriteMoviesByID(int userID);
        Task<bool> GetIsFavoriteMovieByID(int userID, int movieID);

        Task<MovieList> GetFavoriteMoviesByEmail(string email);
        Task RemoveFavoriteMovieByID(int userID, int movieID);
        Task<int> GetFavoriteMovieCount(int movieID);
        Task<List<IdCount>> GetFavoriteMoviesByAgeGroup(int ageGroup);
        Task<List<IdCount>> GetFavoriteMoviesByAll();

        Task<List<Movie>> GetFavoriteMoviesByUser(int userID);
    }
}
