using System.ComponentModel.DataAnnotations;

namespace classroom_booking_backend.DAL.Entities
{
    public class BuildingEntity
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
    }
}
