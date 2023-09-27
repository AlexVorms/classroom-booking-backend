using classroom_booking_backend.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace classroom_booking_backend.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<TeacherEntity> Teacher { get; set; }
        public DbSet<BuildingEntity> Building { get; set; }
        public DbSet<AudiencesEntity> Audiences { get; set; }
        public DbSet<LessonEntity> Lessons { get; set; }
        public DbSet<SheduleEntity> Shedules { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
