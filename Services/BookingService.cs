using classroom_booking_backend.DAL;
using classroom_booking_backend.DAL.Entities;
using classroom_booking_backend.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace classroom_booking_backend.Services
{
    public interface IBookingService
    {
        Task<Boolean> AddBooking(CreateBookingDto model);
        Task<List<BookingDto>> GetUserBooking(string UserId);
        Task<Boolean> ChangeBookingStatus(ChangeBookingStatusDto data);
        Task<BookingDetailsDto> GetBookingDetails(string bookingId);
        Task<Boolean> DeleteBooking(string bookingId);
    }

    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        public BookingService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Boolean> AddBooking(CreateBookingDto model)
        {
            DateTime startModel = DateTime.ParseExact(model.Date + " " + model.Start + ":00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endModel = DateTime.ParseExact(model.Date + " " + model.End + ":00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
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

                    var description = new AddFieldsForBookingEntity
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        Type = DAL.Enums.BookingTypeFieldEnum.DescriptionEvent,
                        Text = model.Description,
                        BookingId = Booking.Id.ToString()
                    };
                    await _context.Bookings.AddAsync(Booking);
                    await _context.FieldsBooking.AddAsync(description);
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
                    ParticipantCount = booking.ParticipantCount,
                    Id = booking.Id.ToString()
                };
                bookingList.Add(b);
            }

            return bookingList;
        }

        public async Task<Boolean> ChangeBookingStatus(ChangeBookingStatusDto data)
        {
            var bookingEntity = await _context
                .Bookings
                .Include(r => r.Audience)
                .Where(a=> a.Id.ToString() == data.BookingId)
                .FirstOrDefaultAsync();

            if (bookingEntity == null)
            {
                return false;
            }
            else
            {
                if (data.Status == DAL.Enums.BookingStatusEnum.Approved)
                {
                    var lesson = new LessonEntity
                    {
                        Id = bookingEntity.Id.ToString(),
                        Starts = bookingEntity.Start,
                        Ends = bookingEntity.End,
                        Date = bookingEntity.Date,
                        Audience = bookingEntity.Audience,
                        LessonType = "BOOKING",
                        Professor = null,
                        Type = "LESSON",
                        Title = bookingEntity.Title,
                        LessonNumber = 2
                    };
                    var lessonEntity = await _context //проверка на наличие подтвержденного бронирования или пары в это время
                    .Lessons
                    .Include(r => r.Audience)
                    .Where(a => (a.Audience.Id == bookingEntity.Audience.Id) && (a.Starts == bookingEntity.Start) && (a.Date == bookingEntity.Date))
                    .FirstOrDefaultAsync();
                    if (lessonEntity == null)
                    {
                        bookingEntity.Status = data.Status;
                        await _context.SaveChangesAsync();

                        var Fields = new AddFieldsForBookingEntity
                        {
                            Id = Guid.NewGuid(),
                            Date = DateTime.Now,
                            BookingId = bookingEntity.Id.ToString(),
                            Type = DAL.Enums.BookingTypeFieldEnum.ResponseBooking,
                            Text = data.Text,
                        };

                        await _context.FieldsBooking.AddAsync(Fields);
                        await _context.Lessons.AddAsync(lesson);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        bookingEntity.Status = DAL.Enums.BookingStatusEnum.Rejected;// если в это время существует отодобренная бронь или пара, бронь автоматически отменяется
                        await _context.SaveChangesAsync();
                        var addFields2 = new AddFieldsForBookingEntity
                        {
                            Id = Guid.NewGuid(),
                            Date = DateTime.Now,
                            BookingId = bookingEntity.Id.ToString(),
                            Type = DAL.Enums.BookingTypeFieldEnum.ResponseBooking,
                            Text = "К сожалению, в это время забронировано другое мероприятие или занятие.",
                        };
                        await _context.FieldsBooking.AddAsync(addFields2);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    bookingEntity.Status = data.Status;
                    await _context.SaveChangesAsync();

                    var addFields = new AddFieldsForBookingEntity
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        Type = DAL.Enums.BookingTypeFieldEnum.ResponseBooking,
                        Text = data.Text,
                        BookingId = bookingEntity.Id.ToString()
                    };

                    await _context.FieldsBooking.AddAsync(addFields);
                    await _context.SaveChangesAsync();
                }
            }
            return true;
        }

        public async Task<BookingDetailsDto> GetBookingDetails(string bookingId)
        {
            var booking = await _context
                .Bookings
                .Include(n => n.Audience)
                .Where(a => a.Id.ToString() == bookingId)
                .FirstOrDefaultAsync();


                var AudienceEntity = await _context
                    .Audiences
                    .Include(r => r.Building)
                    .Where(a => a.Id == booking.Audience.Id)
                    .FirstOrDefaultAsync();

                var FieldsEntity = await _context
                .FieldsBooking
                .Where(a => a.BookingId == bookingId)
                .ToListAsync();

            var description = "";

            var FieldsList = new List<BookingFieldsDto>();
            foreach(var field in FieldsEntity )
            {
                if(field.Type == DAL.Enums.BookingTypeFieldEnum.DescriptionEvent)
                {
                     description = field.Text;
                }
                else
                {
                    var entity = new BookingFieldsDto
                    {
                        Date = field.Date,
                        Type = field.Type.ToString(),
                        Value = field.Text
                    };

                    FieldsList.Add(entity);
                }
            }
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
                var b = new BookingDetailsDto
                {
                    Date = booking.Date,
                    Title = booking.Title,
                    CreatedAt = booking.CreatedAt,
                    Status = booking.Status.ToString(),
                    Start = booking.Date.AddSeconds(booking.Start),
                    End = booking.Date.AddSeconds(booking.End),
                    Audience = audience,
                    ParticipantCount = booking.ParticipantCount,
                    Id = booking.Id.ToString(),
                    Description = description,
                    BookingFields = FieldsList
                };
            return b;
        }

        public async Task<Boolean> DeleteBooking(string bookingId)
        {
            var booking = await _context
                .Bookings
                .Include(n => n.Audience)
                .Where(a => a.Id.ToString() == bookingId)
                .FirstOrDefaultAsync();

            if (booking == null)
            {
                return false;
            }
            else
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
