using classroom_booking_backend.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using classroom_booking_backend.Services;

namespace classroom_booking_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly RestClient _client;
        private ICalendarService _calendarService;
        private IUserService _userService;
        public UsersController(ICalendarService calendarService, IUserService userService)
        {
            _client = new RestClient("https://intime.tsu.ru/api/web/");
            _calendarService = calendarService;
            _userService = userService;
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
            await _calendarService.GetTeachers(results);
            return Ok();
        }
        [HttpGet]
        [Route("/teacher")]
        public async Task<IActionResult> GetTeachers()
        {
            try
            {
                var result = await _calendarService.GetTeachersList();
                if(result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Something went wrong during adding a User model");
            }
        }

        [HttpPost]
        [Route("/addUser")]
        public async Task<IActionResult> AddUser(UserDto user)
        {
            try
            {
                await _userService.RegisterUser(user);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}