using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PreubaLogics.Extensions.Interfaces;
using PreubaLogics.Extensions.Operations;
using PreubaLogics.Interfaces;
using PreubaLogics.Models;
using PruebaDataaces.Datos;
using PruebaDataaces.PruebaDataaces.Persistence.Configuration.dboSchema;
using System.Net;

namespace PreubaLogics.Services
{
    public class PersonaServices : IPersonaServices
    {
        public readonly PruebaContext _context;
        public readonly IMapper _mapper;

        public PersonaServices(PruebaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IOperationResult<PersonaDto>> CreatePerson(PersonaRequest personaDto)
        {
            Persona persona = _mapper.Map<Persona>(personaDto);
            _ = _context.Add(persona);
            _ = await _context.SaveChangesAsync();
            return await persona.ToResultAsync<Persona,PersonaDto>();

        }

        public async Task<IOperationResult> DelettePerson(long id)
        {
            Persona? persona = await _context.Personas.Where(e => e.Activo && e.Id == id).FirstOrDefaultAsync();
            if(persona == null)
            {
                return new OperationResult(HttpStatusCode.BadRequest);

            }
            persona.Activo = false;
            _context.Update(persona);
            await _context.SaveChangesAsync();
            return await persona.ToResultAsync();

        }

        public async Task<IOperationResult<PersonaDto>> GetPerson(string identificacion)
        {
            Persona? persona = await _context.Personas.Where(e => e.Activo&& e.Cedula == identificacion || e.Username == identificacion).FirstOrDefaultAsync();
            return await persona.ToResultAsync<Persona,PersonaDto>();
        }

        public async Task<IOperationResult<PersonaDto>> GetPersonById(long id)
        {
            Persona? persona = await _context.Personas.Where(e => e.Activo && e.Id == id).FirstOrDefaultAsync();
            return await persona.ToResultAsync<Persona, PersonaDto>();
        }

        public async Task<IOperationResultList<PersonaDto>> GetPersonPaginated(PersonaRequest request)
        {
            var personas = _context.Personas.Where(e => e.Activo);
     
            return await personas.ToResultListAsync<Persona, PersonaDto>(request.Offset,request.Take);
        }

        public async Task<IOperationResult<PersonaDto>> updatePerson(long id, PersonaRequest personaDto)
        {
            Persona? persona = await _context.Personas.Where(e => e.Activo&&  e.Id == id).FirstOrDefaultAsync();
            if (persona != null)
            {
                persona = _mapper.Map<Persona>(persona, personaDto);
                _ = _context.Update(persona);
                _ = await _context.SaveChangesAsync();
                return await persona.ToResultAsync<Persona,PersonaDto>();
            }
            return new OperationResult<PersonaDto>(HttpStatusCode.BadRequest);

        }
    }
}
