using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class IdCount
    {
        [JsonPropertyName("MovieID")]
        public int MovieId { get; set; }
        [JsonPropertyName("count")]
        public int count { get; set; }
    }
}
