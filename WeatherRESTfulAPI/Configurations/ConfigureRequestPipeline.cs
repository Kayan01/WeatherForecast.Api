using Microsoft.AspNetCore.Identity;
using Weather.Domain.Entities;
using Weather.Infrastructure.Data;
using Weather.Infrastructure.Seeder;
using WeatherAPI.Middleware;

namespace WeatherAPI.Configurations
{
	public static class ConfigureRequestPipeline
	{

		public static void RequestPipeline(this IApplicationBuilder app, IWebHostEnvironment env)
		{
			var scope = app.ApplicationServices.CreateScope();
			
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

				Seeder.SeedData(roleManager, userManager, dbContext).GetAwaiter();
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseMiddleware<ExceptionMiddleware>();
			app.UseRouting();
			app.UseAuthorization();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
				endpoints.MapControllers());

		}
	}
}
