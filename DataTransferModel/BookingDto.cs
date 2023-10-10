using classroom_booking_backend.DAL.Enums;

namespace classroom_booking_backend.DataTransferModel
{
    public class BookingDto
    {
        public AudiencesForSheduleDto Audience { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public int ParticipantCount { get; set; }
        public string Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
