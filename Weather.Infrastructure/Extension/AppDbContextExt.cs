using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Infrastructure.Data;

namespace Weather.Infrastructure.Extension
{
    public static class AppDbContextExt
    {
        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                getAssembly => getAssembly.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }
    }
}
