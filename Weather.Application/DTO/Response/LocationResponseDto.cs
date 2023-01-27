namespace Weather.Application.DTO.Response
{
	public class LocationResponseDto
	{
		public Guid? Id { get; set; }
		public string Name { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public string TimeZone { get; set; }
	}
}