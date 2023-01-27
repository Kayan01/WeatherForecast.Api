using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Weather.Domain.Entities;
using Weather.Infrastructure.Data;

namespace Weather.Infrastructure.Seeder
{


	public class Seeder
	{
		public static async Task SeedData(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, AppDbContext dbContext)
		{

			await dbContext.Database.EnsureCreatedAsync();

			if (!dbContext.Users.Any())
			{
				List<string> roles = new List<string> { "Admin" };
				foreach (string role in roles)
				{
					await roleManager.CreateAsync(new IdentityRole { Name = role });
				}
				var users = SeederHelper<User>.GetData("User.json");

				/* await dbContext.Users.AddRangeAsync(users);
				 await dbContext.SaveChangesAsync();*/

				foreach (var user in users)
				{
					await userManager.CreateAsync(user, "Admin@001");
					if (user == users[0])
					{
						await userManager.AddToRoleAsync(user, "Admin");
					}
				}

				await dbContext.SaveChangesAsync();
			}
		}
	}
}
