using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Movie
    {
        [JsonPropertyName("genres")]
        public List<Genre> genres { get; set; }
        [JsonPropertyName("id")]
        public int id { get; set; }
        [JsonPropertyName("original_title")]
        public string original_title { get; set; }
        [JsonPropertyName("overview")]
        public string overview { get; set; }
        [JsonPropertyName("poster_path")]
        public string poster_path { get; set; }
        [JsonPropertyName("release_date")]
        public string release_date { get; set; }
        [JsonPropertyName("title")]
        public string title { get; set; }
        [JsonPropertyName("vote_average")]
        public float vote_average { get; set; }
        [JsonPropertyName("vote_count")]
        public int vote_count { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
