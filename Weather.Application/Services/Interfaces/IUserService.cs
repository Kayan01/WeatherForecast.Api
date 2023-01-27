using Weather.Application.DTO.Request;
using Weather.Application.DTO.Response;

namespace Weather.Application.Services.Interfaces
{
	public interface IUserService
	{
		Task<Response<UserResponseDto>> DeleteUserAsync(string userId);
		Task<Response<IEnumerable<UserResponseDto>>> GetAllUsersAsync();
		Task<Response<string>> RegisterUserAsync(RegistrationRequestDto request);
		Task<Response<UserResponseDto>> UpdateUserAsync(string userId, UpdateUserRequestDto request);
		Task<Response<LoginResponseDto>> UserLoginAsync(LoginRequestDto loginRequest);
	}
}