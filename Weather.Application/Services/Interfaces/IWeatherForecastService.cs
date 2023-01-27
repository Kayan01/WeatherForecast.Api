using Weather.Application.DTO.Request;
using Weather.Application.DTO.Response;

namespace Weather.Application.Services.Interfaces
{
	public interface IWeatherForecastService
	{
		Task<Response<string>> CreateWeatherForecastAsync(WeatherForecastRequestDto request);
		Task<Response<WeatherForecastResponseDto>> DeleteWeatherForecastAsync(Guid locationId);
		Task<Response<IEnumerable<WeatherForecastResponseDto>>> GetAllWeatherForecastsAsync();
		Task<Response<WeatherForecastResponseDto>> GetWeatherForecastByIdAsync(Guid id);
		Task<Response<WeatherForecastResponseDto>> UpdateWeatherForecastByIdAsync(UpdateWeatherForecastRequestDto request);
	}
}