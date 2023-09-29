using System.Text.Json.Serialization;

namespace classroom_booking_backend.DataTransferModel
{
    public class LessonsDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("lessonNumber")]
        public int LessonNumber { get; set; }
        [JsonPropertyName("starts")]
        public int Starts { get; set; }
        [JsonPropertyName("ends")]
        public int Ends { get; set; }
        [JsonPropertyName("audience")]
        public AudiencesForSheduleDto Audience { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("professor")]
        public ProfessorDto Professor { get; set; }

        [JsonPropertyName("lessonType")]
        public string LessonType { get; set; }
    }
}
