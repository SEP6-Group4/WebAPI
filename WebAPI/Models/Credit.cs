using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Credit
    {
        [JsonPropertyName("id")]
        public int MovieId { get; set; }

        [JsonPropertyName("cast")]
        public List<Actor> Actors { get; set; }
    }
}
