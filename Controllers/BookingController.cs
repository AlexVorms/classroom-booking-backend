using classroom_booking_backend.DataTransferModel;
using classroom_booking_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace classroom_booking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(CreateBookingDto data)
        {
            try
            {
               
                await _bookingService.AddBooking(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("/{UserId}")]
        public async Task<IActionResult> GetBooking(string UserId)
        {
            try
            {
                var results = await _bookingService.GetUserBooking(UserId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
