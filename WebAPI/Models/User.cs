using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class User
    {
        [JsonPropertyName("UserID")]
        public int? UserID { get; set; }
        [JsonPropertyName("FirstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("LastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("Birthday")]
        public DateOnly? Birthday { get; set; }
        [JsonPropertyName("Email")]
        public string? Email { get; set; }
        [JsonPropertyName("Country")]
        public string? Country { get; set; }
        [JsonPropertyName("Password")]
        public string? Password { get; set; }
    }
}
