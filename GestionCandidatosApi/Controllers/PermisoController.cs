using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GestionCandidatosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisoController : Controller
    {
        private readonly IPermisosService Permisos;

        public PermisoController(IPermisosService _Permisos)
        {
            Permisos = _Permisos;
        }

        [HttpPost]
        [Route("GetAllPermisos")]
        public async Task<ActionResult<List<Permiso>>> GetAllPermisos(Filtros filtro)
        {
            try
            {

                var ListadoPermisos = await Permisos.GetAll(filtro);
                return Ok(ListadoPermisos);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addPermisos")]
        public async Task<ActionResult<string>> InsertUsuario([FromBody] Permiso modelo)
        {
            try
            {
                var result = await Permisos.InsertPermisos(modelo);

                if (result == "Exito")
                {
                    return Ok("Permiso insertado correctamente.");
                }
                else
                {
                    return BadRequest("Hubo un problema al insertar el permiso.");
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y devolvemos un BadRequest con el mensaje de error
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("updatePermiso")]
        public async Task<ActionResult<int>> UpdatePermiso(Permiso modelo)
        {           /**********ACTUALIZAR**********/
            try
            {
                var result = await Permisos.UpdatePermisos(modelo);

                // Validamos el resultado y devolvemos el código HTTP correspondiente
                switch (result)
                {
                    case 0:
                        return Ok("Permiso actualizado correctamente.");
                    case 1:
                        return NotFound("Permiso no encontrado.");
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
        [Route("DeletePermiso")]
        public async Task<ActionResult<string>> DeletePermiso(Permiso modelo)
        {           /***********ELIMINAR************/
            try
            {
                // Llama al servicio para eliminar el candidato
                var result = await Permisos.DeletePermiso(modelo);

                if (result == "Exito")
                {
                    return Ok("Permiso eliminado correctamente.");
                }
                else
                {
                    return NotFound("Permiso no encontrado.");
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
