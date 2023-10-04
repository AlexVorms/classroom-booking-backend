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
        Task<List<LessonsWithDateDto>> GetSheduleInDb(DateTime dateTo, DateTime dateFrom, string AudienceId);
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
                DateTime date1 = DateTime.ParseExact(i.Date + " 07:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                    foreach(var lesson in i.Lessons)
                    {
                   

                        if (lesson.Type != "EMPTY")
                        {
                            var teacher = await _context
                            .Teacher
                            .Where(a => a.Id == lesson.Professor.Id)
                            .FirstOrDefaultAsync();

                            var lessonEntity = await _context
                                .Lessons
                                .Include(r => r.Audience)
                                .Where(a=> (a.Date == date1) && (a.Audience.Id == lesson.Audience.Id) && (a.Starts == lesson.Starts)) 
                                .FirstOrDefaultAsync();

                            if (lessonEntity == null)
                            {
                                if ((teacher != null) && (teacher.ShortName == null))
                                {
                                    teacher.ShortName = lesson.Professor.ShortName;
                                    await _context.SaveChangesAsync();
                                }

                                if (audience.Building.Latitude == null)
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
                                    Title = lesson.Title,
                                    Professor = teacher,
                                    LessonType = lesson.LessonType,
                                    Date = date1,
                                    Audience = audience
                                };

                                await _context.Lessons.AddAsync(Lessons);
                                await _context.SaveChangesAsync();

                            }

                        }
                    }
            }
            return true;
        }
        public async Task<List<LessonsWithDateDto>> GetSheduleInDb(DateTime DateTo, DateTime DateFrom, string AudienceId)
        {

            var lessons = await _context
                .Lessons
                .Include(r=> r.Audience)
                .Include(r => r.Professor)
                .Where(a => (a.Date >= DateFrom) && (a.Date <= DateTo) && (a.Audience.Id == AudienceId))
                .ToListAsync();

            var lessonsList = new List<LessonsWithDateDto>();
            foreach (var lesson in lessons) {

                var professor = new ProfessorDto
                {
                    Id = lesson.Professor.Id,
                    ShortName = lesson.Professor.ShortName,
                    FullName = lesson.Professor.FullName
                };

                var building = await _context
                    .Building
                    .Where(a=> a.Id == lesson.Audience.BuildingId)
                    .FirstOrDefaultAsync();

                var build = new BuildingForSheduleDto
                {
                    Id = building.Id,
                    Address = building.Address,
                    Latitude = building.Latitude,
                    Longitude = building.Longitude,
                    Name = building.Name
                };

                var audience = new AudiencesForSheduleDto
                {
                    Id = lesson.Audience.Id,
                    Name = lesson.Audience.Name,
                    ShortName = lesson.Audience.ShortName,
                    Building = build
                };


                var l = new LessonsWithDateDto
                {
                    Id = lesson.Id,
                    LessonNumber = lesson.LessonNumber,
                    Type = lesson.Type,
                    Start = lesson.Date.AddSeconds(lesson.Starts),
                    End = lesson.Date.AddSeconds(lesson.Ends),
                    Title = lesson.Title,
                    LessonType = lesson.LessonType,
                    Professor = professor,
                    Audience = audience,
                    Date = lesson.Date
                };
                lessonsList.Add(l);
            }
            return lessonsList;
        }
    }
}
