namespace classroom_booking_backend.DataTransferModel
{
    public class BookingDetailsDto
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
        public string Description { get; set; }
        public List<BookingFieldsDto>? BookingFields { get; set; }
    }
}
