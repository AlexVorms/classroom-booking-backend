
using System.ComponentModel.DataAnnotations;


namespace classroom_booking_backend.DAL.Entities
{
    public class SheduleEntity
    {
        [Key]
        [Required]
        public string Date { get; set; }
        [Required]
        public List<LessonEntity> Lessons { get; set; }
    }
}
