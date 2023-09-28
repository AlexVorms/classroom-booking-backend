using classroom_booking_backend.DataTransferModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace classroom_booking_backend.DAL.Entities
{
    public class LessonEntity
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public int LessonNumber { get; set; }
        [Required]
        public int Starts { get; set; }
        [Required]
        public int Ends { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
