using System.Text.Json.Serialization;

namespace classroom_booking_backend.DataTransferModel
{
    public class AudiencesForSheduleDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }
        [JsonPropertyName("building")]
        public BuildingForSheduleDto Building { get; set; }
    }
}
