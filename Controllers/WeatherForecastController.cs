using classroom_booking_backend.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace classroom_booking_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly RestClient _client;

        public UsersController()
        {
            _client = new RestClient("https://intime.tsu.ru/api/web/");
        }


        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var request = new RestRequest("v1/professors");
            var response = await _client.ExecuteGetAsync(request);
            if (!response.IsSuccessful)
            {
                //Logic for handling unsuccessful response
            }

            var json = response.Content;

            List<UserData> results = JsonSerializer.Deserialize<List<UserData>>(json);

            foreach (var tickerMarket in results)
            {
                Console.WriteLine($"{tickerMarket.Id}, {tickerMarket.FullName}");
            }
            return Ok();
        }
    }
}