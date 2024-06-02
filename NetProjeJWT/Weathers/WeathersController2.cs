using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetProjeJWT.Controllers;

namespace NetProjeJWT.Weather
{
    // Role Based Authorization
    [Authorize(Roles ="editor")]
    [Route("api/[controller]")]
    [ApiController]
    public class WeathersController2(IWeatherService weatherService) : CustomBaseController
    {
       
        [HttpGet]
        public IActionResult GetWeather(string city)
        {
            var weather = weatherService.GetWeather(city);

            return Ok(weather);
        }
    }
}
