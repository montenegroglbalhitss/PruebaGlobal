using Microsoft.Extensions.DependencyInjection;
using PreubaLogics.Interfaces;
using PreubaLogics.Mapper;
using PreubaLogics.Services;

namespace PreubaLogics.Configuration
{
    public static class ConfigurationLogics
    {
        public static IServiceCollection AddConfigureCollection(this IServiceCollection services)
        {
            _ = services.AddScoped<IPersonaServices, PersonaServices>();
            services.ConfigureMappingProfile();
            return services;
        }
    }
}
