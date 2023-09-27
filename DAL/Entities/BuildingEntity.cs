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
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
