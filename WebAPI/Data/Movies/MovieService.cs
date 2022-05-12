using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.Data.Movies
{
    public class MovieService : IMovieService
    {
        string url = "https://api.themoviedb.org/3/movie/";
        HttpClient client;

        public MovieService()
        {
            client = new HttpClient();
        }

        public async Task<Movie> GetMovieByID(int id)
        {
            string message = await client.GetStringAsync(url + id + "?api_key=3294e1bdd7442d97a75d3a88e515b933");
            Movie movie = JsonSerializer.Deserialize<Movie>(message);
            return movie;
        }

        public async Task<MovieList> GetMovies(int page)
        {
            var moviesUrl = "top_rated?api_key=3294e1bdd7442d97a75d3a88e515b933&language=en-US&page=";
            if (page != 0)
                moviesUrl += page;
            string message = await client.GetStringAsync(url + moviesUrl);
            MovieList results = JsonSerializer.Deserialize<MovieList>(message);
            return results;
        }

        public async Task<MovieList> GetMoviesBySearch(int page, string query)
        {
            string newUrl = url.Remove(url.IndexOf('3') + 1); //the url is slightly different, so we have to do some string gymnastics here
            Console.WriteLine(newUrl);
            var moviesUrl = newUrl + "/search/movie?api_key=3294e1bdd7442d97a75d3a88e515b933&query=" + query + "&page=" + page;
            string message = await client.GetStringAsync(moviesUrl);
            MovieList results = JsonSerializer.Deserialize<MovieList>(message);
            return results;
        }
    }
}
