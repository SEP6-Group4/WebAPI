﻿using WebAPI.Data.Movies;
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
