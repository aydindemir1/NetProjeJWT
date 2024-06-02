﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetProjeJWT.Controllers;

namespace NetProjeJWT.Weather
{
    // Claims Based Authorization
    [Authorize(Roles ="editor",Policy ="UpdatePolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class WeathersController3(IWeatherService weatherService) : CustomBaseController
    {
       
        [HttpGet]
        public IActionResult GetWeather(string city)
        {
            var weather = weatherService.GetWeather(city);

            return Ok(weather);
        }
    }
}
