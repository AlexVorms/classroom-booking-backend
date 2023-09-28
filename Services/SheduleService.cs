using classroom_booking_backend.DAL;
using classroom_booking_backend.DAL.Entities;
using classroom_booking_backend.DataTransferModel;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace classroom_booking_backend.Services
{
    public interface ISheduleService
    {
        Task<Boolean> AddShedule(List<SheduleDto> results, string AudienceId);
        Task<Boolean> GetSheduleInDb(DateTime dateTo, DateTime dateFrom, string AudienceId);
    }
    public class SheduleService: ISheduleService
    {
        private readonly ApplicationDbContext _context;
        public SheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Boolean> AddShedule(List<SheduleDto> results, string AudienceId)
        {
            var audience = await _context
                                .Audiences
                                .Where(a => a.Id == AudienceId)
                                .Include(r => r.Building)
                                .FirstOrDefaultAsync();

            foreach ( var i in results)
            {
                DateTime date1 = DateTime.ParseExact(i.Date + " 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);

                var SheduleEntity = await _context
                .Shedules
                .Where(a => a.Date == date1)
                .ToListAsync();

                if(SheduleEntity.Count == 0)
                {
                    var listLessons = new List<LessonEntity>();
                    foreach(var lesson in i.Lessons)
                    {
                        if (lesson.Type != "EMPTY")
                        {
                            if(audience.Building == null)
                            {
                                var building = await _context
                                    .Building
                                    .Where(a => a.Id == audience.BuildingId)
                                    .FirstOrDefaultAsync();

                                building.Longitude = lesson.Audience.Building.Longitude;
                                building.Latitude = lesson.Audience.Building.Latitude;
                                building.Address = lesson.Audience.Building.Address;
                                audience.Building = building;
                                audience.ShortName = lesson.Audience.ShortName;
                                await _context.SaveChangesAsync();


                            }
                            var Lessons = new LessonEntity
                            {
                                Type = lesson.Type,
                                LessonNumber = lesson.LessonNumber,
                                Starts = lesson.Starts,
                                Ends = lesson.Ends,
                                Id = lesson.Id, 
                                Title = lesson.Title
                            };
                            listLessons.Add(Lessons);
                        }
                    }
                    await _context.Shedules.AddAsync(new DAL.Entities.SheduleEntity
                    {
                       Date = date1,
                       Lessons = listLessons, 
                       AudienceId = audience.Id
                    });
                    await _context.SaveChangesAsync();
                }
            }
            return true;
        }
        public async Task<Boolean> GetSheduleInDb(DateTime DateTo, DateTime DateFrom, string AudienceId)
        {
            //var shedule = await _context
            //      .Shedules
            //      .Where(a =>( a.Date >= DateTo) && (a.Date <= DateFrom))
            //      .Include(r => r.Lessons)
            //     .ToListAsync();

            //var lessons = await _context
            //    .Lessons
            //     .ToListAsync();
            //var lessons2 = await _context
            //    .Lessons
            //    .Include(r => r.Audience)
            //     .ToListAsync();
            return true;
        }
    }
}
