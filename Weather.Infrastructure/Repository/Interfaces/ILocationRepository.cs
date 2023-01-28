using Weather.Domain.Entities;

namespace Weather.Infrastructure.Repository.Interfaces
{
	public interface ILocationRepository : IGenericRepository<Location>
	{
		Task<IEnumerable<Location>> GetAllLocationsAsync();
	}
}