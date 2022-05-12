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
    }
}
