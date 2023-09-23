using classroom_booking_backend.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace classroom_booking_backend.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<TeacherEntity> Teacher { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
