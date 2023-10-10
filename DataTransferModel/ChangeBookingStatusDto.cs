using classroom_booking_backend.DAL.Enums;

namespace classroom_booking_backend.DataTransferModel
{
    public class ChangeBookingStatusDto
    {
        public string BookingId { get; set; }
        public BookingStatusEnum Status { get; set; }
        public string Text { get; set; }
    }
}
