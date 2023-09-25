using System.Text.Json.Serialization;

namespace classroom_booking_backend.DataTransferModel
{
    public class SheduleDto
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("lessons")]
        public LessonsDto Lessons { get; set; }
    }
}
