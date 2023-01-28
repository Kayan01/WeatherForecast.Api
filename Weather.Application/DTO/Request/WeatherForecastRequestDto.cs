using System.ComponentModel.DataAnnotations;

namespace Weather.Application.DTO.Request
{
	public class WeatherForecastRequestDto
	{
		[Required]
		public Guid LocationId { get; set; }
		[Required]
		public DateTime Date { get; set; }
		[Required]
		public string TemperatureC { get; set; }
		[Required]
		public string TemperatureF { get; set; }
		[Required]
		public string Summary { get; set; }
	}
}