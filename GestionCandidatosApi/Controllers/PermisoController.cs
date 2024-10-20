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
    }
}
