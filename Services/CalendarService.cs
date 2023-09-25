using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using classroom_booking_backend.DAL;
using classroom_booking_backend.DataTransferModel;
using Microsoft.AspNetCore.Http;

namespace classroom_booking_backend.Services
{
    public interface ICalendarService
    {
        Task<Boolean> GetTeachers(List<UserData> results);
        Task<List<UserData>> GetTeachersList();
    }
    public class CalendarService: ICalendarService
    {
        private readonly ApplicationDbContext _context;

        public CalendarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Boolean> GetTeachers(List<UserData> results)
        {
            var TeacherEntity = await _context
                .Teacher
                .FirstOrDefaultAsync();

            if (TeacherEntity != null)
            {
                return false;
            }
            else
            {
                foreach (var teacher in results)
                {

                    await _context.Teacher.AddAsync(new DAL.Entities.TeacherEntity
                    {
                       Id = teacher.Id,
                       FullName = teacher.FullName
                    });
                    await _context.SaveChangesAsync();
                }
                return true;
            }
        }

        public async Task<List<UserData>> GetTeachersList()
        {
            var teachers = await _context
                .Teacher
                .ToListAsync();
            
                var listTeacher = new List<UserData>();
                foreach(var i in teachers)
                {
                    var teacherDto = new UserData
                    {
                        Id = i.Id,
                        FullName = i.FullName,
                    };
                    listTeacher.Add(teacherDto);
                }
                return listTeacher;
        }
    }
}
