using System.Text.Json.Serialization;

namespace classroom_booking_backend.DataTransferModel
{
    public class ProfessorDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }
    }
}
