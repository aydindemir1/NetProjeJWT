using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetProjeJWT.Controllers;

namespace NetProjeJWT.Weather
{
    // Policy Based Authorization
    [Authorize(Policy = "Over18AgePolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class WeathersController4(IWeatherService weatherService) : CustomBaseController
    {
       
        [HttpGet]
        public IActionResult GetWeather(string city)
        {
            var weather = weatherService.GetWeather(city);

            return Ok(weather);
        }
    }
}
