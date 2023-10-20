using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PreubaLogics.Extensions.Operations;
using PreubaLogics.Models;
using PruebaDataaces.PruebaDataaces.Persistence.Configuration.dboSchema;
using System.Reflection;

namespace PreubaLogics.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapSeasons();
        }

        private void MapSeasons()
        {

            #region Request To Entity ;
            _ = CreateMap<PersonaDto, PersonaRequest>().ForAllMembersIfNotEmpty();
            _ = CreateMap<Persona, PersonaRequest>().ForAllMembersIfNotEmpty();
            _ = CreateMap<Persona, Persona>().ForAllMembersIfNotEmpty();
            _ = CreateMap<Persona, PersonaDto>().ForAllMembersIfNotEmpty();

            #endregion
        }
    }

    public static class MappingProfileExtensions
    {
        public static void ConfigureMappingProfile(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
