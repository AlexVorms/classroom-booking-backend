using classroom_booking_backend.DataTransferModel;
using classroom_booking_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Globalization;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace classroom_booking_backend.Controllers
{
    [Route("shedule")]
    [ApiController]
    public class SheduleController : ControllerBase
    {
        private readonly RestClient _client;
        private ISheduleService _sheduleService;
        public SheduleController(ISheduleService sheduleService)
        {
            _client = new RestClient("https://intime.tsu.ru/api/web/");
            _sheduleService = sheduleService;
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
                    await _sheduleService.AddShedule(results, id);
                    DateTime dateFrom1 = DateTime.ParseExact(dateFrom + " 07:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dateTo1 = DateTime.ParseExact(dateTo + " 07:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                    var result = await _sheduleService.GetSheduleInDb(dateTo1, dateFrom1, id);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("/getshedule")]
        public async Task<IActionResult> GetShedule(string id, string dateFrom, string dateTo)
        {
            try
            {
                DateTime dateFrom1 = DateTime.ParseExact(dateFrom + " 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dateTo1 = DateTime.ParseExact(dateTo + " 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                var result = await _sheduleService.GetSheduleInDb(dateTo1, dateFrom1, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

    } 
}
