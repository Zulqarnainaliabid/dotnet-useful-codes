using EfCoreDbIterceptors.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EfCoreDbIterceptors.Code
{
    public static class StartupExtentions
    {
        public static IServiceCollection UseEFInterceptor<T>(this IServiceCollection services, IConfiguration configuration)
            where T : class, IInterceptor
        {
            return services
                .AddScoped<DbContextOptions>((serviceProvider) =>
                {
                    var tenant = serviceProvider.GetRequiredService<TenantDetail>();

                    var efServices = new ServiceCollection();
                    efServices.AddEntityFrameworkNpgsql();
                    efServices.AddScoped<TenantDetail>(s =>
                        serviceProvider.GetRequiredService<TenantDetail>()); 
                    efServices.AddScoped<IInterceptor, T>(); 

                    var connectionString = configuration.GetConnectionString("DefaultConnection");

                    return new DbContextOptionsBuilder<EntitiesContext>()
                        .UseInternalServiceProvider(efServices.BuildServiceProvider())
                        .UseNpgsql(connectionString)
                        .Options;
                })
                .AddScoped<EntitiesContext>(s => new EntitiesContext(s.GetRequiredService<DbContextOptions>()));
        }
    }
}
