using classroom_booking_backend.DAL.Enums;

namespace classroom_booking_backend.DataTransferModel
{
    public class BookingFieldsDto
    {
       public string Value { get; set; }
       public string Type { get; set; }
       public DateTime Date { get; set; }

    }
}
