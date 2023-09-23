using System.ComponentModel.DataAnnotations;

namespace classroom_booking_backend.DAL.Entities
{
    public class TeacherEntity
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(4)]
        public string FullName { get; set; }
    }
}
