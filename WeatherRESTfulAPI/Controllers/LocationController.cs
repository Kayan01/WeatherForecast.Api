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
	public class LocationController : Controller
	{
		private readonly ILocationService _locationService;
		public LocationController(ILocationService locationService)
		{
			_locationService = locationService;
		}
		
		[HttpPost]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
		public async Task<IActionResult> CreateLocation(LocationRequestDto request)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var response = await _locationService.CreateLocationAsync(request);
				return StatusCode((int)response.ResponseCode, response);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.BadRequest, Response<string>.Fail("Failed", ex.Message, HttpStatusCode.BadRequest));
			}
		}
		[HttpGet]
		[Route("{locationId}")]
		public async Task<IActionResult> GetLocationById(Guid locationId)
		{
			var response = await _locationService.GetLocationByIdAsync(locationId);
			return StatusCode((int)response.ResponseCode, response);

		}
		[HttpGet]
		public async Task<IActionResult> GetAllLocations()
		{
			var response = await _locationService.GetAllLocationsAsync();
			return StatusCode((int)response.ResponseCode, response);

		}
		[HttpPut]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
		public async Task<IActionResult> UpdateLocation(UpdateLocationRequestDto request)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var response = await _locationService.UpdateLocationByIdAsync(request);
				return StatusCode((int)response.ResponseCode, response);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.BadRequest, Response<string>.Fail("Failed", ex.Message, HttpStatusCode.BadRequest));
			}
		}

		[HttpDelete]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
		public async Task<IActionResult> DeleteLocationById(Guid locationId)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var response = await _locationService.DeleteUserAsync(locationId);
				return StatusCode((int)response.ResponseCode, response);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.BadRequest, Response<string>.Fail("Failed", ex.Message, HttpStatusCode.BadRequest));
			}
		}

	}
}
