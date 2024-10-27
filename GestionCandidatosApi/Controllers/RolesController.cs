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
                    return Ok("Rol insertado correctamente.");
                }
                else
                {
                    return BadRequest("Hubo un problema al insertar el Rol.");
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y devolvemos un BadRequest con el mensaje de error
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("updateRol")]
        public async Task<ActionResult<int>> UpdateRol(Roles modelo)
        {           /**********ACTUALIZAR**********/
            try
            {
                var result = await roles.UpdateRoles(modelo);

                // Validamos el resultado y devolvemos el código HTTP correspondiente
                switch (result)
                {
                    case 0:
                        return Ok("Rol actualizado correctamente.");
                    case 1:
                        return NotFound("Rol no encontrado.");
                    default:
                        return StatusCode(500, "Error desconocido.");
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y devolvemos un BadRequest con el mensaje de error
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("DeleteRol")]
        public async Task<ActionResult<string>> DeleteRol(Roles modelo)
        {           /***********ELIMINAR************/
            try
            {
                // Llama al servicio para eliminar el candidato
                var result = await roles.DeleteRol(modelo);

                if (result == "Exito")
                {
                    return Ok("Rol eliminado correctamente.");
                }
                else
                {
                    return NotFound("Rol no encontrado.");
                }
            }
            catch (Exception ex)
            {
                // Captura la excepción y devuelve un BadRequest con el mensaje de error
                return BadRequest(ex.Message);
            }
        }
    }
}
