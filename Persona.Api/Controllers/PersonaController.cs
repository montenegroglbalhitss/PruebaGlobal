
using Microsoft.AspNetCore.Mvc;
using Persona.Api.CommonHttp;
using PreubaLogics.Extensions.Interfaces;
using PreubaLogics.Interfaces;
using PreubaLogics.Models;

namespace Persona.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaServices _services;

        public PersonaController(IPersonaServices services)
        {
            _services = services;
        }

        [HttpPost("create-person")]
        [ProducesResponseType(typeof(IOperationResult<PersonaDto>), 200)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> CreatePerson(PersonaRequest personaDto)
        {
            try
            {

                IOperationResult<PersonaDto> result = await _services.CreatePerson(personaDto);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {

                return ex.ToObjectResult();
            }
        }


        [HttpPut("{id}/update-person")]
        [ProducesResponseType(typeof(IOperationResult<PersonaDto>), 200)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> updatePerson(long id, PersonaRequest personaDto)
        {
            try
            {

                IOperationResult<PersonaDto> result = await _services.updatePerson(id, personaDto);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {

                return ex.ToObjectResult();
            }
        }

        [HttpGet("get-person/{identificacion}")]
        [ProducesResponseType(typeof(IOperationResult<PersonaDto>), 200)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> GetPerson(string identificacion)
        {
            try
            {

                IOperationResult<PersonaDto> result = await _services.GetPerson(identificacion);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {

                return ex.ToObjectResult();
            }
        }

        [HttpGet("get-person-byId/{id}")]
        [ProducesResponseType(typeof(IOperationResult<PersonaDto>), 200)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> GetPersonById(long id)
        {
            try
            {

                IOperationResult<PersonaDto> result = await _services.GetPersonById(id);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {

                return ex.ToObjectResult();
            }
        }

        [HttpDelete("delte-person/{id}")]
        [ProducesResponseType(typeof(IOperationResult<PersonaDto>), 200)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> DelettePerson(long id)
        {
            try
            {

                IOperationResult result = await _services.DelettePerson(id);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {

                return ex.ToObjectResult();
            }
        }
        [HttpGet("get-list-persons")]
        [ProducesResponseType(typeof(IOperationResultList<PersonaDto>), 200)]
        [ProducesResponseType(typeof(IOperationResult), 500)]
        public async Task<IActionResult> GetPersonPaginated([FromQuery] PersonaRequest request)
        {
            try
            {

                IOperationResultList<PersonaDto> result = await _services.GetPersonPaginated(request);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {

                return ex.ToObjectResult();
            }
        }

    }
}
