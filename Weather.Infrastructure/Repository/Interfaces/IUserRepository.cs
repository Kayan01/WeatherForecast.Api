using Weather.Domain.Entities;

namespace Weather.Infrastructure.Repository.Interfaces
{
	public interface IUserRepository : IGenericRepository<User>
	{
		Task<IEnumerable<User>> GetAllUsersAsync();
	}
}