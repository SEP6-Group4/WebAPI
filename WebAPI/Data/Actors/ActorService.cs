using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.Data.Actors
{
    public class ActorService : IActorService
    {
        string url = "https://api.themoviedb.org/3/person/";
        HttpClient client;
        private readonly IConfiguration configuration;
        private string apiKey;

        public ActorService(IConfiguration iConfig)
        {
            client = new HttpClient();
            configuration = iConfig;
            apiKey = configuration["APIKeys:ApiKey"];
        }

        public async Task<Actor> GetActorByID(int id)
        {
            string message = await client.GetStringAsync(url + id + apiKey);
            Actor movie = JsonSerializer.Deserialize<Actor>(message);
            return movie;
        }

        public async Task<MovieCredit> GetMovieCreditsByActorId(int actorId)
        {
            string message = await client.GetStringAsync(url + "/" + actorId + "/movie_credits" + apiKey);
            MovieCredit result = JsonSerializer.Deserialize<MovieCredit>(message);
            return result;
        }

        public async Task<ActorList> GetPopularActors(int page)
        {
            string message = await client.GetStringAsync(url + "popular" + apiKey + "&language=en-US&page=" + page);
            ActorList result = JsonSerializer.Deserialize<ActorList>(message);
            return result;
        }

        public async Task<ActorList> GetActorsBySearch(int page, string query)
        {
            string newUrl = url.Remove(url.IndexOf('3') + 1); //the url is slightly different, so we have to do some string gymnastics here
            Console.WriteLine(newUrl);
            var moviesUrl = newUrl + "/search/person" + apiKey + "&query=" + query + "&page=" + page;
            string message = await client.GetStringAsync(moviesUrl);
            ActorList results = JsonSerializer.Deserialize<ActorList>(message);
            return results;
        }
    }
}
