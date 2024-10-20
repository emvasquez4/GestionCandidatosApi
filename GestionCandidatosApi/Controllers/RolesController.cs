using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services.Utilidades;
using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCandidatosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        public readonly IRolesService roles;
        
        public RolesController(IRolesService _roles)
        {
            roles = _roles;
        }

        [HttpPost]
        [Route("GetAllroles")]
        public async Task<ActionResult<List<Roles>>> GetAllroles(Filtros filtro)
        {
            try
            {

                var Listadoroles = await roles.GetAll(filtro);
                return Ok(Listadoroles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addRoles")]
        public async Task<ActionResult<string>> InsertUsuario([FromBody] Roles modelo)
        {
            try
            {
                var result = await roles.InsertRoles(modelo);

                if (result == "Exito")
                {
                    return Ok("Menu insertado correctamente.");
                }
                else
                {
                    return BadRequest("Hubo un problema al insertar el usuario.");
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y devolvemos un BadRequest con el mensaje de error
                return BadRequest(ex.Message);
            }
        }
    }
}
