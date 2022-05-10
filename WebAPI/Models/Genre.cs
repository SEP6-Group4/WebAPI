using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Genre
    {
        [JsonPropertyName("id")]
        public int id { get; set; }
        [JsonPropertyName("name")]
        public string name { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
