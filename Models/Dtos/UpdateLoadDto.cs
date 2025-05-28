using System.Text.Json.Serialization;

namespace AlmoxManagerApi.Models.Dtos
{
    public class UpdateLoadDto
    {
        public string? Material { get; set; }
        [JsonPropertyName("average_weight")]
        public int AverageWeight { get; set; }
        public string? Unit { get; set; }
        [JsonPropertyName("created_at")]
        public required string CreatedAt { get; set; }
        public string? Operator { get; set; } 
        public string? Shift { get; set; }
    }
}