using System.Net;
using Weather.Application.DTO.Request;
using Weather.Application.DTO.Response;
using Weather.Application.Services.Interfaces;
using Weather.Domain.Entities;
using Weather.Infrastructure.Repository.Interfaces;

namespace Weather.Application.Services.Implementations
{
	public class WeatherForecastService : IWeatherForecastService
	{
		private readonly IGenericRepository<WeatherForecast> _weatherRepository;
		public WeatherForecastService(IGenericRepository<WeatherForecast> weatherRepository)
		{
			_weatherRepository = weatherRepository;
		}

		public async Task<Response<string>> CreateWeatherForecastAsync(WeatherForecastRequestDto request)
		{
			var weatherForecast = new WeatherForecast()
			{
				TemperatureC = request.TemperatureC,
				TemperatureF = request.TemperatureF,
				Date = request.Date,
				Summary = request.Summary
			};
			var result = await _weatherRepository.AddAsync(weatherForecast);
			if (result)
			{
				return Response<string>.Success("Location successfully created", String.Empty, HttpStatusCode.NoContent);
			}
			return Response<string>.Fail("Failed to create location");
		}

		public async Task<Response<WeatherForecastResponseDto>> UpdateWeatherForecastByIdAsync(UpdateWeatherForecastRequestDto request)
		{
			var weatherForecast = _weatherRepository.TableNoTracking.FirstOrDefault(weatherForecast => weatherForecast.Id == request.WeatherForecastId);
			if (weatherForecast is null)
			{
				return Response<WeatherForecastResponseDto>.Fail($"Location with id {request.WeatherForecastId} does not exist", HttpStatusCode.NotFound);
			}
			weatherForecast.TemperatureC = String.IsNullOrEmpty(request.TemperatureC) ? weatherForecast.TemperatureC : request.TemperatureC;
			weatherForecast.TemperatureF = String.IsNullOrEmpty(request.TemperatureC) ? weatherForecast.TemperatureF : request.TemperatureF;
			weatherForecast.Summary = string.IsNullOrEmpty(request.Summary) ? weatherForecast.Summary : request.Summary;
			weatherForecast.UpdatedAt = DateTime.Now;
			var result = await _weatherRepository.UpdateAsync(weatherForecast);
			if (result)
			{
				var response = new WeatherForecastResponseDto()
				{
					Id = weatherForecast.Id,
					TemperatureC = weatherForecast.TemperatureC,
					TemperatureF = weatherForecast.TemperatureF,
					Date = weatherForecast.Date,
					Summary = weatherForecast.Summary
				};
				return Response<WeatherForecastResponseDto>.Success("Updated", response, HttpStatusCode.NoContent);
			}
			return Response<WeatherForecastResponseDto>.Fail("Failed to update");
		}
		public async Task<Response<WeatherForecastResponseDto>> GetWeatherForecastByIdAsync(Guid id)
		{
			var weatherForecast = await _weatherRepository.GetARecordAsync(id);
			if (weatherForecast is null)
			{
				Response<WeatherForecastResponseDto>.Fail($"Location with id {id} not found", HttpStatusCode.NotFound);
			}
			var response = new WeatherForecastResponseDto()
			{
				Id = weatherForecast.Id,
				TemperatureC = weatherForecast.TemperatureC,
				TemperatureF = weatherForecast.TemperatureF,
				Date = weatherForecast.Date,
				Summary = weatherForecast.Summary
			};
			return Response<WeatherForecastResponseDto>.Success("Success", response);
		}
		public async Task<Response<IEnumerable<WeatherForecastResponseDto>>> GetAllWeatherForecastsAsync()
		{
			var weatherForecasts = await _weatherRepository.GetAllRecordAsync();
			var response = new List<WeatherForecastResponseDto>();
			foreach (var weatherForecast in weatherForecasts)
			{
				response.Add(new WeatherForecastResponseDto()
				{
					Id = weatherForecast.Id,
					TemperatureC = weatherForecast.TemperatureC,
					TemperatureF = weatherForecast.TemperatureF,
					Date = weatherForecast.Date,
					Summary = weatherForecast.Summary
				});
			}
			return Response<IEnumerable<WeatherForecastResponseDto>>.Success("Success", response);
		}
		public async Task<Response<WeatherForecastResponseDto>> DeleteWeatherForecastAsync(Guid locationId)
		{
			var weatherForecast = _weatherRepository.TableNoTracking.FirstOrDefault(location => location.Id == locationId);
			if (weatherForecast is null)
			{
				return Response<WeatherForecastResponseDto>.Fail($"User with id {locationId} does not exist", HttpStatusCode.NotFound);
			}
			weatherForecast.IsDeleted = true;
			weatherForecast.DeletedAt = DateTime.Now;
			var result = await _weatherRepository.UpdateAsync(weatherForecast);
			if (result)
			{
				var response = new WeatherForecastResponseDto()
				{
					Id = weatherForecast.Id,
					TemperatureC = weatherForecast.TemperatureC,
					TemperatureF = weatherForecast.TemperatureF,
					Date = weatherForecast.Date,
					Summary = weatherForecast.Summary
				};
				return Response<WeatherForecastResponseDto>.Success("Deleted", response, HttpStatusCode.OK);
			}
			return Response<WeatherForecastResponseDto>.Fail("Failed to delete user");
		}
	}
}

