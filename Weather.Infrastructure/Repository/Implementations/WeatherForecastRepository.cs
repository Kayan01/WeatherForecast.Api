using Microsoft.EntityFrameworkCore;
using System;
using Weather.Domain.Entities;
using Weather.Infrastructure.Data;
using Weather.Infrastructure.Repository.Interfaces;

namespace Weather.Infrastructure.Repository.Implementations
{
	public class WeatherForecastRepository : GenericRepository<WeatherForecast>, IWeatherForecastRepository
	{
		public WeatherForecastRepository(AppDbContext dbContext) : base(dbContext)
		{

		}

		public async Task<IEnumerable<WeatherForecast>> GetAllWeatherForecastsByLocationIdAsync(Guid? locationId)
		{
			return await this._dbSet.Where(weatherForecast => weatherForecast.LocationId == locationId && weatherForecast.IsDeleted == false).ToListAsync();
		}
	}
}
