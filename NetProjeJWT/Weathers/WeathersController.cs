using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetProjeJWT.Controllers;

namespace NetProjeJWT.Weather
{
    // Client Credential Authentication
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeathersController(IWeatherService weatherService) : CustomBaseController
    {
       
        [HttpGet]
        public IActionResult GetWeather(string city)
        {
            var weather = weatherService.GetWeather(city);

            return Ok(weather);
        }
    }
}
