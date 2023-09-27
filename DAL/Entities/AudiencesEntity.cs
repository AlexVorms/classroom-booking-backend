using System.ComponentModel.DataAnnotations;

namespace classroom_booking_backend.DAL.Entities
{
    public class AudiencesEntity
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        public string BuildingId { get; set; }

        public string? ShortName { get; set; }
        public BuildingEntity? Building { get; set; }
    }
}
