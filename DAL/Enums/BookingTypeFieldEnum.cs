using System.ComponentModel.DataAnnotations;

namespace classroom_booking_backend.DAL.Enums
{
    public enum BookingTypeFieldEnum
    {

        [Display(Name = TypeNames.PhoneNumber)]
        PhoneNumber,
        [Display(Name = TypeNames.DescriptionEvent)]
        DescriptionEvent,
        [Display(Name = TypeNames.ResponseBooking)]
        ResponseBooking
    }

    public class TypeNames
    {
        public const string PhoneNumber = "PHONE NUMBER";
        public const string DescriptionEvent = "DESCRIPTION EVENT";
        public const string ResponseBooking = "RESPONSE TO BOOKING";
    }
}
