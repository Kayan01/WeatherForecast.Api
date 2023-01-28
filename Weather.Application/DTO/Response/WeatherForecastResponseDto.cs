namespace Weather.Application.DTO.Response
{
	public class WeatherForecastResponseDto
	{
		public Guid? Id { get; set; }
		public Guid? LocationId { get; set; }
		public DateTime? Date { get; set; }
		public string? TemperatureC { get; set; }
		public string? TemperatureF { get; set; }
		public string? Summary { get; set; }
	}
}