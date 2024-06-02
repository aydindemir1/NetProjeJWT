using NetProjeJWT.SharedDTOs;

namespace NetProjeJWT.Weather
{
    public interface IWeatherService
    {
        ResponseModelDto<int> GetWeather(string city);
    }

    public class WeatherService : IWeatherService
    {
        public ResponseModelDto<int> GetWeather(string city)
        {
            return ResponseModelDto<int>.Success(20);
        }
    }
}
