using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace AlmoxManagerApi.Models.Dtos
{
    [Keyless]
    public class LoadSummaryDto
    {
        public string? Material { get; set; }
        [JsonPropertyName("total_weight")]
        public int TotalWeight { get; set; }
    }
}