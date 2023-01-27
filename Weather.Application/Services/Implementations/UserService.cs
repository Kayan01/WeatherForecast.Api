using Microsoft.AspNetCore.Identity;
using System.Net;
using Weather.Application.DTO.Request;
using Weather.Application.DTO.Response;
using Weather.Application.Services.Interfaces;
using Weather.Domain.Entities;
using Weather.Infrastructure.Repository.Interfaces;

namespace Weather.Application.Services.Implementations
{
	public class UserService : IUserService
	{

		private readonly UserManager<User> _userManager;
		private readonly IGenericRepository<User> _userRepository;
		private readonly ITokenGenerator _tokenGenerator;
		public UserService(UserManager<User> userManager, ITokenGenerator tokenGenerator, 
			IGenericRepository<User>  userRepository)
		{
			_userManager = userManager;
			_tokenGenerator = tokenGenerator;
			_userRepository = userRepository;
		}

		public async Task<Response<LoginResponseDto>> UserLoginAsync(LoginRequestDto loginRequest)
		{
			var user = await _userManager.FindByEmailAsync(loginRequest.Email);
			if(user is null)
			{
				return Response<LoginResponseDto>.Fail("Email does not exist", HttpStatusCode.NotFound);
			}
			if(!await _userManager.CheckPasswordAsync(user, loginRequest.Password))
			{
				return Response<LoginResponseDto>.Fail("Incorrect password");
			}
	   var token = 	await	_tokenGenerator.GenerateToken(user);
			var response = new LoginResponseDto()
			{
				Token = token
			};
			return Response<LoginResponseDto>.Success("Success", response);
		}

		public async Task<Response<string>> RegisterUserAsync(RegistrationRequestDto request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			if(user != null)
			{
				return Response<string>.Success("Failed", "Email has been used", HttpStatusCode.BadRequest);
			}
			var registeruUser = new User()
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				UserName = request.Email
			};
			var result = await _userManager.CreateAsync(registeruUser, request.Password);
			if (result.Succeeded)
			{
				return Response<string>.Success("Success", "", HttpStatusCode.NoContent);
			}

			var errors = result?.Errors.Aggregate(string.Empty, (current, error) => current + (error.Description + Environment.NewLine));
			return Response<string>.Fail(errors!);
		}

		public async Task<Response<IEnumerable<UserResponseDto>>> GetAllUsersAsync()
		{
			var users = await _userRepository.GetAllRecordAsync();
			var response = new List<UserResponseDto>();
			foreach(var user in users)
			{
				response.Add(new UserResponseDto()
				{
					Id = user.Id,
					FirstName = user.FirstName!,
					LastName = user.LastName!,
					UserName = user.UserName
				});
			}
			return Response<IEnumerable<UserResponseDto>>.Success("Success", response);
		}
		public async Task<Response<UserResponseDto>> UpdateUserAsync(string userId, UpdateUserRequestDto request)
		{
			var user = _userRepository.TableNoTracking.FirstOrDefault(user => user.Id == userId);
			if (user is null)
			{
				return Response<UserResponseDto>.Fail($"User with id {userId} does not exist", HttpStatusCode.NotFound);
			}
			user.FirstName = String.IsNullOrEmpty(request.FirstName) ? user.FirstName : request.FirstName;
			user.LastName = String.IsNullOrEmpty(request.LastName) ? user.LastName : request.LastName;
			user.UserName = String.IsNullOrEmpty(request.UserName) ? user.UserName : request.UserName;
			user.UpdatedAt = DateTime.Now;
			var result = await _userRepository.UpdateAsync(user);
			if (result)
			{
				var response = new UserResponseDto()
				{
					Id = user.Id,
					FirstName = user.FirstName!,
					LastName = user.LastName!,
					UserName = user.UserName,
				};
				return Response<UserResponseDto>.Success("Updated", response, HttpStatusCode.OK);
			}
			return Response<UserResponseDto>.Fail("Failed to update user details");
		}

		public async Task<Response<UserResponseDto>> DeleteUserAsync(string userId)
		{
			var user = _userRepository.TableNoTracking.FirstOrDefault(user => user.Id == userId);
			if (user is null)
			{
				return Response<UserResponseDto>.Fail($"User with id {userId} does not exist", HttpStatusCode.NotFound);
			}
			user.IsDeleted = true;
			user.DeletedAt = DateTime.Now;
			var result = await _userRepository.UpdateAsync(user);
			if (result)
			{
				var response = new UserResponseDto()
				{
					Id = user.Id,
					FirstName = user.FirstName!,
					LastName = user.LastName!,
					UserName = user.UserName,
				};
				return Response<UserResponseDto>.Success("Deleted", response, HttpStatusCode.OK);
			}
			return Response<UserResponseDto>.Fail("Failed to delete user");
		}
	}
}
