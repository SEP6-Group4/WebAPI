using WebAPI.Models;

namespace WebAPI.Persistence.FavoriteMovie
{
    public interface IFavoriteMovieRepo
    {
        /// <summary>
        /// Adds the provided movie to the provided user's favorite movies
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="movieID"></param>
        /// <returns></returns>
        Task AddFavoriteMovie(int userID, int movieID);

        /// <summary>
        /// Returns a list of movie IDs that the specified userID has marked as favorite
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<int>> GetFavoriteMoviesByID(int userID);

        /// <summary>
        /// Returns true or false depending if movie is marked as favorite by provided user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="movieID"></param>
        /// <returns></returns>
        Task<bool> GetIsFavoriteMovieByID(int userID, int movieID);

        /// <summary>
        /// Removes provided favorite movie from the provided user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="movieID"></param>
        /// <returns></returns>
        Task RemoveFavoriteMovieByID(int userID, int movieID);
    }
}
