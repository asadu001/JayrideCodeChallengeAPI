using System.Text.Json.Serialization;

namespace JayrideCodeChallengeAPI.Models
{
    public class IpLocationResponse
    {
        [JsonPropertyName("ip")]
        public string Ip { get; set; } = default!;
        [JsonPropertyName("city")]
        public string City { get; set; } = default!;
    }
}
