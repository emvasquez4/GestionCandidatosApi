using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCandidatosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Roles_PermisosController : Controller
    {
        public readonly IRolesPermisosService roles;

        public Roles_PermisosController(IRolesPermisosService _roles)
        {
            roles = _roles;
        }

        [HttpPost]
        [Route("GetAllRoles_Permisos")]
        public async Task<ActionResult<List<Roles_Permisos>>> GetAllRoles_Permisos(Filtros filtro)
        {
            try
            {

                var ListadoRoles_Permisos = await roles.GetAll(filtro);
                return Ok(ListadoRoles_Permisos);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
