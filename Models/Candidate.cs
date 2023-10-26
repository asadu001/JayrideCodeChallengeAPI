using System.Text.Json.Serialization;

namespace JayrideCodeChallengeAPI.Models
{
    public class Candidate
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
        [JsonPropertyName("phone")]
        public string Phone { get; set; } = default!;
    }
}
