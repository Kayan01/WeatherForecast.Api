using Weather.Domain.Entities;

namespace Weather.Infrastructure.Repository.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		IQueryable<T> TableNoTracking { get; }

		Task<bool> AddAsync(T entity);
		Task<bool> AddRangeAsync(IEnumerable<T> entities);
		Task<bool> DeleteAsync(T entity);
		Task<bool> DeleteRangeAsync(IEnumerable<T> entities);
		Task<IEnumerable<T>> GetAllRecordAsync();
		Task<T> GetARecordAsync(string Id);
		Task<T> GetARecordAsync(Guid Id);
		Task<bool> UpdateAsync(T entity);
		Task<bool> UpdateRangeAsync(IEnumerable<T> entities);
	}
}