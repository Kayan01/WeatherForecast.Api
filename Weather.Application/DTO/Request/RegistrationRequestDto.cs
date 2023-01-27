using System.ComponentModel.DataAnnotations;

namespace Weather.Application.DTO.Request
{
	public class RegistrationRequestDto
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Text)]
		public string FirstName { get; set; }
		[Required]
		[DataType(DataType.Text)]
		public string LastName { get; set; }
		[Required]
		[DataType(DataType.Text)]
		[MinLength(7, ErrorMessage = "Password should be at least 7 characters long")]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Text)]
		[Compare("Password",ErrorMessage = "Confirm password and password do not match")]
		public string ConfirmPassword { get; set; }
	}
}