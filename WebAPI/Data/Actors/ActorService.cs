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
    }
}
