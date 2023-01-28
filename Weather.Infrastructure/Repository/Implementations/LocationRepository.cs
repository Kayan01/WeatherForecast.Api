using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weather.Domain.Entities;
using Weather.Infrastructure.Data;
using Weather.Infrastructure.Repository.Interfaces;

namespace Weather.Infrastructure.Repository.Implementations
{
	public class LocationRepository : GenericRepository<Location>, ILocationRepository
	{
		public LocationRepository(AppDbContext dbContext) : base(dbContext)
		{

		}

		public async Task<IEnumerable<Location>> GetAllLocationsAsync()
		{
			return await this._dbSet.Where(location => location.IsDeleted == false).ToListAsync();
		}
	}
}
