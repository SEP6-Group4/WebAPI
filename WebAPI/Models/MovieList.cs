using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class MovieList
    {
        [JsonPropertyName("page")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("results")]
        public List<Movie>? Movies { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPage { get; set; }
    }
}