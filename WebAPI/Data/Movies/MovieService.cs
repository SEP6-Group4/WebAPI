using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.Data.Movies
{
    public class MovieService : IMovieService
    {
        string url = "https://api.themoviedb.org/3/movie/";
        HttpClient client;
        private readonly IConfiguration configuration;
        private string apiKey;

        public MovieService(IConfiguration iConfig)
        {
            client = new HttpClient();
            configuration = iConfig;
            apiKey = configuration["APIKeys:ApiKey"];
        }

        public async Task<Movie> GetMovieByID(int id)
        {
            string message = await client.GetStringAsync(url + id + apiKey);
            Movie movie = JsonSerializer.Deserialize<Movie>(message);
            return movie;
        }

        public async Task<MovieList> GetMovies(int page)
        {
            var moviesUrl = "top_rated" + apiKey + "&language=en-US&page=";
            if (page != 0)
                moviesUrl += page;
            string message = await client.GetStringAsync(url + moviesUrl);
            MovieList results = JsonSerializer.Deserialize<MovieList>(message);
            return results;
        }
    }
}
