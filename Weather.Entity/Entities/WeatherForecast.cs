namespace Weather.Domain.Entities
{
	public class WeatherForecast : BaseEntity
	{
		public DateTime? Date { get; set; }

		public string? TemperatureC { get; set; }

		public string? TemperatureF { get; set; }

		public string? Summary { get; set; }
		public Location? Location { get; set; }
	}
}