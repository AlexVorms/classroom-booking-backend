using System.Text.Json.Serialization;

namespace classroom_booking_backend.DataTransferModel
{
    public class AudiencesDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
