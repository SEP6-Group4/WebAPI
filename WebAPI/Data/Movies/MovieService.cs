using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.Data.Movies
{
    public class MovieService : IMovieService
    {
        string url = "https://api.themoviedb.org/3/movie/";
        string newUrl = "https://api.themoviedb.org/3/discover/movie";
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

        public async Task<MovieList> GetMoviesByRatingDesc(int page)
        {
            var moviesUrl = apiKey + "&language=en-US&sort_by=vote_average.desc&vote_count.gte=215&vote_average.gte=3&page=";
            if (page != 0)
                moviesUrl += page;
            string message = await client.GetStringAsync(newUrl + moviesUrl);
            MovieList results = JsonSerializer.Deserialize<MovieList>(message);
            return results;
        }

        public async Task<MovieList> GetMoviesByRatingAsc(int page)
        {
            var moviesUrl = apiKey + "&language=en-US&sort_by=vote_average.asc&vote_count.gte=215&vote_average.gte=3&page=";
            if (page != 0)
                moviesUrl += page;
            string message = await client.GetStringAsync(newUrl + moviesUrl);
            MovieList results = JsonSerializer.Deserialize<MovieList>(message);
            return results;
        }

        public async Task<MovieList> GetMoviesByTitleAsc(int page)
        {
            var moviesUrl = apiKey + "&language=en-US&sort_by=original_title.asc&vote_count.gte=215&vote_average.gte=3&page=";
            if (page != 0)
                moviesUrl += page;
            string message = await client.GetStringAsync(newUrl + moviesUrl);
            MovieList results = JsonSerializer.Deserialize<MovieList>(message);
            return results;
        }

        public async Task<MovieList> GetMoviesByTitleDesc(int page)
        {
            var moviesUrl = apiKey + "&language=en-US&sort_by=original_title.desc&vote_count.gte=215&vote_average.gte=3&page=";
            if (page != 0)
                moviesUrl += page;
            string message = await client.GetStringAsync(newUrl + moviesUrl);
            MovieList results = JsonSerializer.Deserialize<MovieList>(message);
            return results;
        }


        public async Task<Credit> GetCreditsByMovieId(int movieId)
        {
            string message = await client.GetStringAsync(url + movieId + "/credits" + apiKey + "&language=en-US");
            Credit result = JsonSerializer.Deserialize<Credit>(message);
            return result;
        }
        public async Task<MovieList> GetMoviesBySearch(int page, string query)
        {
            string newUrl = url.Remove(url.IndexOf('3') + 1); //the url is slightly different, so we have to do some string gymnastics here
            Console.WriteLine(newUrl);
            var moviesUrl = newUrl + "/search/movie" + apiKey + "&query=" + query + "&page=" + page;
            string message = await client.GetStringAsync(moviesUrl);
            MovieList results = JsonSerializer.Deserialize<MovieList>(message);
            return results;
        }
    }
}
