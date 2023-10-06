using classroom_booking_backend.DAL;
using classroom_booking_backend.DAL.Entities;
using classroom_booking_backend.DataTransferModel;
using Microsoft.EntityFrameworkCore;

namespace classroom_booking_backend.Services
{
    public interface IUserService
    {
        Task<Boolean> RegisterUser(UserDto model);
    }

    public class UserService: IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Boolean> RegisterUser(UserDto model)
        {
            var userEntity = await _context
           .User
           .Where(x => x.Email == model.Email && x.Password == model.Password)
           .FirstOrDefaultAsync();

            if (userEntity != null)
            {
                return false;
            }
            else
            {
                var userModel = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Email = model.Email,
                    Password = model.Password,
                };
                await _context.User.AddAsync(userModel);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
    
}
