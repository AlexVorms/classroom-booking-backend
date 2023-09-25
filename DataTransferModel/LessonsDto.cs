using System.Text.Json.Serialization;

namespace classroom_booking_backend.DataTransferModel
{
    public class LessonsDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        public int LessonNumber { get; set; }
        public int Starts { get; set; } 
        public int Ends { get; set; }
        public AudiencesDto Audience { get; set; }

    }
}
