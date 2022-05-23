using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class ActorList
    {
        [JsonPropertyName("page")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("results")]
        public List<Actor>? Actors { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPage { get; set; }
    }
}
