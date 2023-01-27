using System.ComponentModel.DataAnnotations;

namespace Weather.Application.DTO.Request
{
	public class LoginRequestDto
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		
		[Required]
		[DataType(DataType.Password)]
		[MinLength(7, ErrorMessage = "Password should be at least 7 characters long")]
		public string Password { get; set; }
	}
}