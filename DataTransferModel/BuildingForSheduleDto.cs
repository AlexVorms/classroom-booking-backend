using System.Text.Json.Serialization;

namespace classroom_booking_backend.DataTransferModel
{
    public class BuildingForSheduleDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }
    }
}
