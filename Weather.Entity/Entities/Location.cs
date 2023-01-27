namespace Weather.Domain.Entities
{
	public class Location : BaseEntity
	{
		public string? Name { get; set; }
		public string? Latitude { get; set; }
		public string? Longitude { get; set; }
		public string? TimeZone { get; set; }
		public ICollection<WeatherForecast>? Forecasts { get; set; }
	}
}
