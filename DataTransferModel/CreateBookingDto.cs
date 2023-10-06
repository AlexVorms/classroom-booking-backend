namespace classroom_booking_backend.DataTransferModel
{
    public class CreateBookingDto
    {

        public string AudienceId { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public int ParticipantCount { get; set; }

        public string Start { get; set; }
        public string End { get; set; }
        public string UserId { get; set; }

    }
}
