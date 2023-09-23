using System.Text.Json.Serialization;

namespace classroom_booking_backend.DataTransferModel
{
    public class UserData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        
    }
}
