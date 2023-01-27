using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Application.DTO.Request
{
	public class UpdateLocationRequestDto
	{
		[Required]
		public Guid LocationId { get; set; }
		public string Name { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public string TimeZone { get; set; }
	}
}
