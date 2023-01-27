using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Weather.Application.DTO.Request;
using Weather.Application.DTO.Response;
using Weather.Application.Services.Interfaces;

namespace WeatherRESTfulAPI.Controllers
{

	[Route("[controller]/[action]")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class WeatherForecastController : Controller
	{
		private readonly IWeatherForecastService _weatherService;
		public WeatherForecastController(IWeatherForecastService weatherService)
		{
			_weatherService = weatherService;
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
		public async Task<IActionResult> CreateWeatherForecast(WeatherForecastRequestDto request)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
					var response = await _weatherService.CreateWeatherForecastAsync(request);
				return StatusCode((int)response.ResponseCode, response);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.BadRequest, Response<string>.Fail("Failed", ex.Message, HttpStatusCode.BadRequest));
			}
		}
		[HttpGet]
		[Route("{weatherForecastId}")]
		public async Task<IActionResult> GetWeatherForecastById(Guid weatherForecastId)
		{
			var response = await _weatherService.GetWeatherForecastByIdAsync(weatherForecastId);
			return StatusCode((int)response.ResponseCode, response);

		}
		[HttpGet]
		public async Task<IActionResult> GetAllWeatherForecasts()
		{
			var response = await _weatherService.GetAllWeatherForecastsAsync();
			return StatusCode((int)response.ResponseCode, response);

		}

		[HttpPut]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
		public async Task<IActionResult> UpdateWeatherForecast(UpdateWeatherForecastRequestDto request)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var response = await _weatherService.UpdateWeatherForecastByIdAsync(request);
				return StatusCode((int)response.ResponseCode, response);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.BadRequest, Response<string>.Fail("Failed", ex.Message, HttpStatusCode.BadRequest));
			}
		}


		[HttpDelete]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
		public async Task<IActionResult> DeleteWeatherForecastById(Guid weatherForecastId)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var response = await _weatherService.DeleteWeatherForecastAsync(weatherForecastId);
				return StatusCode((int)response.ResponseCode, response);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.BadRequest, Response<string>.Fail("Failed", ex.Message, HttpStatusCode.BadRequest));
			}
		}
	}
}
