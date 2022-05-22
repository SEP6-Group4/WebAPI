using WebAPI.Models;

namespace WebAPI.Persistence.FavoriteMovie
{
    public interface IFavoriteMovieRepo
    {
        Task AddFavoriteMovie(int userID, int movieID);
     
        Task<List<int>> GetFavoriteMoviesByID(int userID);
    
        Task<bool> GetIsFavoriteMovieByID(int userID, int movieID);

        Task RemoveFavoriteMovieByID(int userID, int movieID);

        Task<int> GetFavoriteMovieCount(int movieID);

        Task<List<IdCount>> GetFavoriteMoviesByAll();

        Task<List<IdCount>> GetFavoriteMoviesByAgeGroup(int ageGroup);
    }
}
