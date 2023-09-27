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
                                .FirstOrDefaultAsync();

            foreach ( var i in results)
            {
                var SheduleEntity = await _context
                .Shedules
                .Where(a => a.Date == i.Date)
                .ToListAsync();

                if(SheduleEntity.Count == 0)
                {
                    var listLessons = new List<LessonEntity>();
                    foreach(var lesson in i.Lessons)
                    {
                        if (lesson.Type == "EMPTY")
                        {
                                var EmptyLessons = new LessonEntity
                                {
                                    Type = lesson.Type,
                                    LessonNumber = lesson.LessonNumber,
                                    Starts = lesson.Starts,
                                    Ends = lesson.Ends,
                                    Audience = audience
                                };
                                listLessons.Add(EmptyLessons);
                            
                        }
                        else
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
                                Audience = audience,
                                Id = lesson.Id, 
                                Title = lesson.Title,
                            };
                            listLessons.Add(Lessons);
                        }
                    }

                    
                    await _context.Shedules.AddAsync(new DAL.Entities.SheduleEntity
                    {
                       Date = i.Date,
                       Lessons = listLessons
                    });
                    await _context.SaveChangesAsync();
                }
            }
            return true;
        }
    }
}
