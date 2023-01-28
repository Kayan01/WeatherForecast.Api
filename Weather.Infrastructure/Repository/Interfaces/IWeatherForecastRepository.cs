using Weather.Domain.Entities;

namespace Weather.Infrastructure.Repository.Interfaces
{
	public interface IWeatherForecastRepository : IGenericRepository<WeatherForecast>
	{
		Task<IEnumerable<WeatherForecast>> GetAllWeatherForecastsByLocationIdAsync(Guid? locationId);
	}
}