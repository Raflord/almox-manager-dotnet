using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace AlmoxManagerApi.Models.Dtos
{
    [Keyless]
    public class LoadFilteredDto
    {
        public string? Material { get; set; }
        [JsonPropertyName("first_date")]
        public string? FirstDate { get; set; }
        [JsonPropertyName("seccond_date")]
        public string? SeccondDate { get; set;}
    }
}