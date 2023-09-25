using classroom_booking_backend.DataTransferModel;
using classroom_booking_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace classroom_booking_backend.Controllers
{
    [Route("shedule")]
    [ApiController]
    public class SheduleController : ControllerBase
    {
        private readonly RestClient _client;

        public SheduleController()
        {
            _client = new RestClient("https://intime.tsu.ru/api/web/");
        }

        [HttpGet]
        public async Task<IActionResult> AddShedule(string id, string dateFrom, string dateTo)
        {
            try
            {
                var request = new RestRequest("v1/schedule/audience?id=" + id + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo);
                var response = await _client.ExecuteGetAsync(request);
                if (!response.IsSuccessful)
                {
                    return StatusCode(500);
                }
                else
                {
                    var json = response.Content;
                    List<SheduleDto> results = JsonSerializer.Deserialize<List<SheduleDto>>(json);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}
