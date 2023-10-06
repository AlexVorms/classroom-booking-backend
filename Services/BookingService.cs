using classroom_booking_backend.DAL;
using classroom_booking_backend.DAL.Entities;
using classroom_booking_backend.DataTransferModel;
using Microsoft.EntityFrameworkCore;

namespace classroom_booking_backend.Services
{
    public interface IBookingService
    {
        Task<Boolean> AddBooking(CreateBookingDto model);
        Task<List<BookingDto>> GetUserBooking(string UserId);
    }

    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        public BookingService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Boolean> AddBooking(CreateBookingDto model)
        {
            DateTime startModel = DateTime.ParseExact(model.Start, "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endModel = DateTime.ParseExact(model.End, "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dateModel = DateTime.ParseExact(model.Date + " 07:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
            int start = (startModel.Hour - 7)*60*60 + (startModel.Minute * 60);
            int end = (endModel.Hour - 7) * 60 * 60 + (endModel.Minute * 60);

            var lesson = await _context //проверка на наличие подтвержденного бронирования или пары в это время
                .Lessons
                .Include(r => r.Audience)
                .Where(a => (a.Audience.Id == model.AudienceId) && (a.Starts == start) && (a.Date == dateModel)) //нужно дописать условие
                .FirstOrDefaultAsync();

            if(lesson == null)
            {
               

                var booking = await _context // проверка на наличие такой же брони у пользователя 
                    .Bookings
                     .Include(r => r.User)
                    .Where(a=> (a.User.Id.ToString() == model.UserId) && (a.Date == dateModel) && (a.Start == start))
                    .ToListAsync();

                if(booking.Count == 0)
                {
                    var audience = await _context
                        .Audiences
                         .Include(r => r.Building)
                        .Where(a => a.Id == model.AudienceId)
                        .FirstOrDefaultAsync();

                    var user = await _context
                        .User
                        .Where(a => a.Id.ToString() == model.UserId)
                        .FirstOrDefaultAsync();

                    

                    //var lessonEntity = new LessonEntity
                    //{
                    //    Id = Guid.NewGuid().ToString(),
                    //    Type = "BOOKING",
                    //    Title = model.Title,
                    //    Audience = audience,
                    //    Professor = null,
                    //    LessonType = null, 
                    //    Starts= start,
                    //    Ends= end,
                    //    LessonNumber = 0,
                    //    Date = dateModel

                    //};

                    var Booking = new BookingEntity
                    {
                        Id = Guid.NewGuid(),
                        Title = model.Title,
                        ParticipantCount = model.ParticipantCount,
                        CreatedAt = DateTime.Now,
                        Status = DAL.Enums.BookingStatusEnum.New,
                        Audience = audience,
                        User = user,
                        Start = start,
                        End = end,
                        Date = dateModel
                    };

                    await _context.Bookings.AddAsync(Booking);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public async Task<List<BookingDto>> GetUserBooking(string UserId)
        {
            var bookings = await _context
                .Bookings
                .Include(r => r.User)
                .Include(n => n.Audience)
                .Where(a => a.User.Id.ToString() == UserId)
                .ToListAsync();

            var bookingList = new List<BookingDto>();

            foreach (var booking in bookings) {

                var AudienceEntity = await _context
                    .Audiences
                    .Include(r => r.Building)
                    .Where(a => a.Id == booking.Audience.Id)
                    .FirstOrDefaultAsync();

                var build = new BuildingForSheduleDto
                {
                    Id = AudienceEntity.Building.Id,
                    Address = AudienceEntity.Building.Address,
                    Latitude = AudienceEntity.Building.Latitude,
                    Longitude = AudienceEntity.Building.Longitude,
                    Name = AudienceEntity.Building.Name
                };

                var audience = new AudiencesForSheduleDto
                {
                    Id = AudienceEntity.Id,
                    Name = AudienceEntity.Name,
                    ShortName = AudienceEntity.ShortName,
                    Building = build
                };
                var b = new BookingDto
                {
                    Date = booking.Date,
                    Title = booking.Title,
                    CreatedAt = booking.CreatedAt,
                    Status = booking.Status.ToString(),
                    Start = booking.Date.AddSeconds(booking.Start),
                    End = booking.Date.AddSeconds(booking.End),
                    Audience = audience,
                    ParticipantCount = booking.ParticipantCount
                };
                bookingList.Add(b);
            }

            return bookingList;
        }
    }
}
