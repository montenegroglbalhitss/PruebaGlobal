using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaDataaces.Datos;

namespace PruebaDataaces.Configuration
{
    public static class DataaccesConfigure
    {
        public static IServiceCollection AddConfigureDataAcces(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddDbContext<PruebaContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultDatabase")));

            return services;

        }
    }
}
