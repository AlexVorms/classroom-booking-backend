using classroom_booking_backend.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace classroom_booking_backend.DAL.Entities
{
    public class AddFieldsForBookingEntity
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Text { get; set; }
      
        [Required]
        public BookingTypeFieldEnum Type { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string BookingId { get; set; }
    }
}
