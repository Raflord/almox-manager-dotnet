using System.Text.Json.Serialization;
using MySqlConnector;

namespace AlmoxManagerApi.Models.Entities
{
    public class Load
    {
        public string Id { get; set; } = Ulid.NewUlid().ToString();
        public string? Material { get; set; }
        [JsonPropertyName("average_weight")]
        public int? AverageWeight { get; set; }
        public string? Unit { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }
        public string? Operator { get; set; } 
        public string? Shift { get; set; }
    }
}