using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Weather.Application.DTO.Request;
using Weather.Application.DTO.Response;
using Weather.Application.Services.Interfaces;

namespace WeatherRESTfulAPI.Controllers
{
	[Route("[controller]/[action]")]

	[Authorize(AuthenticationSchemes = "Bearer")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		public UserController(IUserService userService)
		{
			_userService = userService;
		}
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> UserLogin(LoginRequestDto request)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var response = await _userService.UserLoginAsync(request);
				return StatusCode((int)response.ResponseCode,response);
			}
			catch(Exception ex)
			{
				return StatusCode((int)HttpStatusCode.BadRequest, Response<string>.Fail("Failed", ex.Message, HttpStatusCode.BadRequest));
			}
		}
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> UserRegisteration(RegistrationRequestDto request)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var response = await _userService.RegisterUserAsync(request);
				return StatusCode((int)response.ResponseCode, response);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.BadRequest, Response<string>.Fail("Failed", ex.Message, HttpStatusCode.BadRequest));
			}
		}
		[HttpPut]
		public async Task<IActionResult> UpdateUserById(UpdateUserRequestDto request)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
					var userId = HttpContext.User.FindFirst(user => user.Type == ClaimTypes.NameIdentifier).Value;
				var response = await _userService.UpdateUserAsync(userId,request);
				return StatusCode((int)response.ResponseCode, response);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.BadRequest, Response<string>.Fail("Failed", ex.Message, HttpStatusCode.BadRequest));
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteUSerById()
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var userId = HttpContext.User.FindFirst(user => user.Type == ClaimTypes.NameIdentifier).Value;
				var response = await _userService.DeleteUserAsync(userId);
				return StatusCode((int)response.ResponseCode, response);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.BadRequest, Response<string>.Fail("Failed", ex.Message, HttpStatusCode.BadRequest));
			}
		}

		[HttpDelete]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
		public async Task<IActionResult> AdminDeleteUSerById(string userId)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var response = await _userService.DeleteUserAsync(userId);
				return StatusCode((int)response.ResponseCode, response);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.BadRequest, Response<string>.Fail("Failed", ex.Message, HttpStatusCode.BadRequest));
			}
		}

		[HttpGet]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
		public async Task<IActionResult> GetAllUsers()
		{
		
				var response = await _userService.GetAllUsersAsync();
				return StatusCode((int)response.ResponseCode, response);
			
		}
	}
}
