using PreubaLogics.Extensions.Interfaces;
using PreubaLogics.Models;

namespace PreubaLogics.Interfaces
{
    public interface IPersonaServices
    {
        Task<IOperationResult<PersonaDto>> CreatePerson(PersonaRequest personaDto);
        Task<IOperationResult> DelettePerson(long id);
        Task<IOperationResult<PersonaDto>> GetPerson(string identificacion);
        Task<IOperationResult<PersonaDto>> GetPersonById(long id);
        Task<IOperationResultList<PersonaDto>> GetPersonPaginated(PersonaRequest request);
        Task<IOperationResult<PersonaDto>> updatePerson(long id, PersonaRequest personaDto);
    }
}
