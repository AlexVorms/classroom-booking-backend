using classroom_booking_backend.DataTransferModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace classroom_booking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public BookingController()
        {

        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(CreateBookingDto data)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
