using System.Net;
using Weather.Application.DTO.Request;
using Weather.Application.DTO.Response;
using Weather.Application.Services.Interfaces;
using Weather.Domain.Entities;
using Weather.Infrastructure.Repository.Interfaces;

namespace Weather.Application.Services.Implementations
{
	public class LocationService : ILocationService
	{
		private readonly ILocationRepository _locationRepository;
		public LocationService(ILocationRepository locationRepository)
		{
			_locationRepository = locationRepository;
		}

		public async Task<Response<string>> CreateLocationAsync(LocationRequestDto request)
		{
			var location = new Location()
			{
				Name = request.Name,
				Latitude = request.Latitude,
				Longitude = request.Longitude,
				TimeZone = request.TimeZone
			};
			var result = await _locationRepository.AddAsync(location);
			if (result)
			{
				return Response<string>.Success("Location successfully created",String.Empty, HttpStatusCode.NoContent);
			}
			return Response<string>.Fail("Failed to create location");
		}

		public async Task<Response<LocationResponseDto>> UpdateLocationByIdAsync(UpdateLocationRequestDto request)
		{
			var location =  _locationRepository.TableNoTracking.FirstOrDefault(location => location.Id == request.LocationId);
			if(location is null)
			{
				return Response<LocationResponseDto>.Fail($"Location with id {request.LocationId} does not exist",HttpStatusCode.NotFound);
			}
			location.Name = string.IsNullOrEmpty(request.Name) ? location.Name : request.Name;
			location.Latitude = string.IsNullOrEmpty(request.Latitude) ? location.Latitude : request.Latitude;
			location.Longitude = string.IsNullOrEmpty(request.Longitude) ? location.Longitude : request.Longitude;
			location.TimeZone = string.IsNullOrEmpty(request.TimeZone) ? location.TimeZone : request.TimeZone;
			location.UpdatedAt = DateTime.Now;
			var result = await _locationRepository.UpdateAsync(location);
			if (result)
			{
				var response = new LocationResponseDto()
				{
					Id = location.Id,
					Name = location.Name,
					Latitude = location.Latitude,
					Longitude = location.Longitude,
					TimeZone = location.TimeZone
				};
				return Response<LocationResponseDto>.Success("Updated", response, HttpStatusCode.NoContent);
			}
			return Response<LocationResponseDto>.Fail("Failed to update");
		}
		public async Task<Response<LocationResponseDto>> GetLocationByIdAsync(Guid id)
		{
			var location = await _locationRepository.GetARecordAsync(id);
			if(location is null)
			{
				Response<Location>.Fail($"Location with id {id} not found", HttpStatusCode.NotFound);
			}
			var response = new LocationResponseDto()
			{
				Id = location.Id,
				Name = location.Name,
				Latitude = location.Latitude,
				Longitude = location.Longitude,
				TimeZone = location.TimeZone
			};
			return Response<LocationResponseDto>.Success("Success", response);
		}
		public async Task<Response<IEnumerable<LocationResponseDto>>> GetAllLocationsAsync()
		{
			var locations = await _locationRepository.GetAllLocationsAsync();
			var response = new List<LocationResponseDto>();
			foreach (var location in locations)
			{
				response.Add(new LocationResponseDto()
				{
					Id = location.Id,
					Name = location.Name,
					Latitude = location.Latitude,
					Longitude = location.Longitude,
					TimeZone = location.TimeZone
				});
			}
			return Response<IEnumerable<LocationResponseDto>>.Success("Success", response);
		}
		public async Task<Response<LocationResponseDto>> DeleteUserAsync(Guid locationId)
		{
			var location = _locationRepository.TableNoTracking.FirstOrDefault(location => location.Id == locationId);
			if (location is null)
			{
				return Response<LocationResponseDto>.Fail($"Location with id {locationId} does not exist", HttpStatusCode.NotFound);
			}
			location.IsDeleted = true;
			location.DeletedAt = DateTime.Now;
			var result = await _locationRepository.UpdateAsync(location);
			if (result)
			{
				var response = new LocationResponseDto()
				{
					Id = location.Id,
					Name = location.Name!,
					Latitude = location.Latitude!,
					Longitude = location.Longitude,
					TimeZone = location.TimeZone
				};
				return Response<LocationResponseDto>.Success("Deleted", response, HttpStatusCode.OK);
			}
			return Response<LocationResponseDto>.Fail("Failed to delete location");
		}
	}
}
