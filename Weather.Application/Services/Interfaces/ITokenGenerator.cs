using Weather.Domain.Entities;

namespace Weather.Application.Services.Interfaces
{
	public interface ITokenGenerator
	{
		Task<string> GenerateToken(User user);
	}
}