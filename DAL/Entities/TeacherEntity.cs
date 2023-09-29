using System.ComponentModel.DataAnnotations;

namespace classroom_booking_backend.DAL.Entities
{
    public class TeacherEntity
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [MinLength(4)]
        public string FullName { get; set; }
        public string? ShortName { get; set; }
    }
}
