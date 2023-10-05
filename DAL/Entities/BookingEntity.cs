using classroom_booking_backend.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace classroom_booking_backend.DAL.Entities
{
    public class BookingEntity
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int ParticipantCount { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public BookingStatusEnum Status { get; set; }
        [Required]
        public AudiencesEntity Audience { get; set; }
        [Required]
        public UserEntity User { get; set; }
        [Required]
        public LessonEntity Lesson { get; set; }
    }
}
