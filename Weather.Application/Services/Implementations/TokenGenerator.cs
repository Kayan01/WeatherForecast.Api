using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Weather.Application.Services.Interfaces;
using Weather.Domain.Entities;

namespace Weather.Application.Services.Implementations
{
	public class TokenGenerator : ITokenGenerator
	{
		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _config;
		public TokenGenerator(UserManager<User> userManager, IConfiguration config)
		{
			_userManager = userManager;
			_config = config;
		}
		public async Task<string> GenerateToken(User user)
		{
			var authClaims = new List<Claim>
						{
								new Claim(ClaimTypes.NameIdentifier, user.Id),
								new Claim(ClaimTypes.Email, user.Email)
						};
			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				authClaims.Add(new Claim(ClaimTypes.Role, role));
			}

			{
				var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:SecretKey"]));
				var token = new JwtSecurityToken
				(
				issuer: _config["JWTSettings:Issuer"],
				claims: authClaims,
				expires: DateTime.Now.AddMinutes(10),
				signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
				);
				return new JwtSecurityTokenHandler().WriteToken(token);
			}
		}
	}
}
