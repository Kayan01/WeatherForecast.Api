using Microsoft.AspNetCore.Identity;
using Weather.Application.Services.Implementations;
using Weather.Application.Services.Interfaces;
using Weather.Domain.Entities;
using Weather.Infrastructure.Data;
using Weather.Infrastructure.Repository.Implementations;
using Weather.Infrastructure.Repository.Interfaces;

namespace WeatherAPI.Configurations
{
	public static class DIServiceExtension
	{
		public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
		{
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<ILocationService, LocationService>();
      services.AddScoped<IWeatherForecastService, WeatherForecastService>();
      services.AddScoped<ITokenGenerator, TokenGenerator>();
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
      services.AddScoped<ILocationRepository, LocationRepository>();
      services.AddMvcCore();
      services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();
      services.Configure<IdentityOptions>(options =>
      {
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = true;
      });
      services.Configure<DataProtectionTokenProviderOptions>(opt =>
       opt.TokenLifespan = TimeSpan.FromMinutes(10));

    }
  }
}
