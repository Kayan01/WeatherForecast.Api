using System.ComponentModel.DataAnnotations;

namespace Weather.Application.DTO.Request
{
	public class LocationRequestDto
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Latitude { get; set; }
		[Required]
		public string Longitude { get; set; }
		[Required]
		public string TimeZone { get; set; }
	}
}