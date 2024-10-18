using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionCandidatosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisoController : Controller
    {
        public readonly IPermisosService Permisos;

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
    }
}
