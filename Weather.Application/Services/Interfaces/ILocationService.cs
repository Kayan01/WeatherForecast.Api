using Weather.Application.DTO.Request;
using Weather.Application.DTO.Response;

namespace Weather.Application.Services.Interfaces
{
	public interface ILocationService
	{
		Task<Response<string>> CreateLocationAsync(LocationRequestDto request);
		Task<Response<LocationResponseDto>> DeleteUserAsync(Guid locationId);
		Task<Response<IEnumerable<LocationResponseDto>>> GetAllLocationsAsync();
		Task<Response<LocationResponseDto>> GetLocationByIdAsync(Guid id);
		Task<Response<LocationResponseDto>> UpdateLocationByIdAsync(UpdateLocationRequestDto request);
	}
}