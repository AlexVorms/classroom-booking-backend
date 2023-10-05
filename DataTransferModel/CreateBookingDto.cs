namespace classroom_booking_backend.DataTransferModel
{
    public class CreateBookingDto
    {
        public string AudienceId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public int ParticipantCount { get; set; }

        public int Start { get; set; }
        public int End { get; set; }

    }
}
