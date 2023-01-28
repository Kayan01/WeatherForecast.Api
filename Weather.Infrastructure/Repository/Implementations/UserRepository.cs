using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Entities;
using Weather.Infrastructure.Data;
using Weather.Infrastructure.Repository.Interfaces;

namespace Weather.Infrastructure.Repository.Implementations
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(AppDbContext dbContext) : base(dbContext)
		{

		}

		public async Task<IEnumerable<User>> GetAllUsersAsync()
		{
			return await this._dbSet.Where(user => user.IsDeleted == false).ToListAsync();
		}
	}
}
