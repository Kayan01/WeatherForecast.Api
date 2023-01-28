using System.ComponentModel.DataAnnotations;

namespace Weather.Application.DTO.Request
{
	public class UpdateWeatherForecastRequestDto
	{
		[Required]
		public Guid WeatherForecastId { get; set; }

		public string TemperatureC { get; set; }

		public string TemperatureF { get; set; }

		public string Summary { get; set; }
	}
}