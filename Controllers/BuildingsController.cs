using classroom_booking_backend.DataTransferModel;
using classroom_booking_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace classroom_booking_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class BuildingsController : ControllerBase
    {
        private readonly RestClient _client;
        private IBuildingService _buildingService;
        public BuildingsController( IBuildingService buildingService )
        {
            _client = new RestClient("https://intime.tsu.ru/api/web/");
            _buildingService= buildingService;
        }

        [HttpGet]
        public async Task<IActionResult> AddBuildings()
        {
            try
            {
                var request = new RestRequest("v1/buildings");
                var response = await _client.ExecuteGetAsync(request);
                if (!response.IsSuccessful)
                {
                    return StatusCode(500);
                }
                else
                {
                    var json = response.Content;
                    List<BuildingDto> results = JsonSerializer.Deserialize<List<BuildingDto>>(json);
                    await _buildingService.AddBuildingsInDB(results);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("/buildings")]
        public async Task<IActionResult> GetBuilding()
        {
            try
            {
                var result = await _buildingService.GetBuildingList();
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("/audiences/{id}")]

        public async Task<IActionResult> AddAudiences(string id)
        {
            try
            {
                var request = new RestRequest("v1/buildings/" + id + "/audiences");
                var response = await _client.ExecuteGetAsync(request);
                if (!response.IsSuccessful)
                {
                    return StatusCode(500);
                }
                else
                {
                    var json = response.Content;
                    List<AudiencesDto> results = JsonSerializer.Deserialize<List<AudiencesDto>>(json);
                   await _buildingService.AddAudiences(id, results);
                    var result = await _buildingService.GetAudiencesList(id);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(result);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("/audiences/get")]
        public async Task<IActionResult> GetAudiences(string id)
        {
            try
            {
                var results = await _buildingService.GetAudiencesList(id);
                if (results == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(results);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
