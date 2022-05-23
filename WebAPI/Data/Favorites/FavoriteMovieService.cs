using WebAPI.Data.Movies;
using WebAPI.Models;
using WebAPI.Persistence.FavoriteMovie;
using WebAPI.Persistence.User;

namespace WebAPI.Data.Favorites
{
    public class FavoriteMovieService : IFavoriteMovieService
    {
        IFavoriteMovieRepo repo;
        IMovieService movieService;
        IUserRepo userRepo;

        public FavoriteMovieService(IConfiguration configuration)
        {
            repo = new FavoriteMovieRepo(configuration);
            movieService = new MovieService(configuration);
            userRepo = new UserRepo(configuration);
        }

        public async Task AddFavoriteMovie(int userID, int movieID)
        {
            await repo.AddFavoriteMovie(userID, movieID);
        }

        public async Task<MovieList> GetFavoriteMoviesByEmail(string email)
        {
            Models.User user = await userRepo.GetUserAsync(email);

            if (user.FavouritePrivacy == true) return null;

            return await GetFavoriteMoviesByID((int)user.UserID);
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

        public async Task<List<IdCount>> GetFavoriteMoviesByAll()
        {
            List<IdCount> moveIDs = await repo.GetFavoriteMoviesByAll();
            return moveIDs;
        }

        public async Task<List<Movie>> GetFavoriteMoviesByUser(int userID)
        {
            var movieIdList = await repo.GetFavoriteMoviesByID(userID);

            List<Movie> toReturn = new List<Movie>();
            
            foreach (var movieId in movieIdList)
            {
                toReturn.Add(await movieService.GetMovieByID(movieId));
            }
            return toReturn;
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
