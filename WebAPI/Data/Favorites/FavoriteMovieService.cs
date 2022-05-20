using WebAPI.Data.Movies;
using WebAPI.Models;
using WebAPI.Persistence.FavoriteMovie;

namespace WebAPI.Data.Favorites
{
    public class FavoriteMovieService : IFavoriteMovieService
    {
        IFavoriteMovieRepo repo;
        IMovieService movieService;

        public FavoriteMovieService(IConfiguration configuration)
        {
            repo = new FavoriteMovieRepo(configuration);
            movieService = new MovieService(configuration);
        }

        public async Task AddFavoriteMovie(int userID, int movieID)
        {
            await repo.AddFavoriteMovie(userID, movieID);
        }

        public async Task<int> GetFavoriteMovieCount(int movieID)
        {
            int count = await repo.GetFavoriteMovieCount(movieID);
            return count;
        }

        public async Task<List<IdCount>> GetFavoriteMoviesByAgeGroup(int ageGroup)
        {
            List<IdCount> moveIDs = await repo.GetFavoriteMoviesByAgeGroup(ageGroup);
            return moveIDs;
        }

        public async Task<MovieList> GetFavoriteMoviesByID(int userID)
        {
            var movieList = await repo.GetFavoriteMoviesByID(userID);

            MovieList toReturn = new MovieList
            {
                CurrentPage = 1,
                Movies = new List<Movie>()
            };
            foreach (var movie in movieList)
            {
                toReturn.Movies.Add(await movieService.GetMovieByID(movie));
            }
            float pageNum = (float)toReturn.Movies.Count / 20;
            toReturn.TotalPage = (int)Math.Ceiling(pageNum);
            return toReturn;
        }

        public async Task<bool> GetIsFavoriteMovieByID(int userID, int movieID)
        {
            return await repo.GetIsFavoriteMovieByID(userID, movieID);
        }

        public async Task RemoveFavoriteMovieByID(int userID, int movieID)
        {
            await repo.RemoveFavoriteMovieByID(userID, movieID);
        }
    }
}
