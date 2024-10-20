using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace GestionCandidatosApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class Usuarios_RolesController : Controller
    {
        private readonly IUsuariosRolesService usuariosRoles;
        private readonly Context dbContext;
        public Usuarios_RolesController(IUsuariosRolesService _usuariosRoles, Context _dbContext)
        {
            usuariosRoles = _usuariosRoles;
            dbContext = _dbContext;
        }


        [HttpPost]
        [Route("GetAllUR")]
        public async Task<ActionResult<List<Usuarios_Roles>>> GetusuariosRoles(Filtros filtro)
        {
            try
            {

                var Listado = await usuariosRoles.GetAll(filtro);
                return Ok(Listado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("GetUserRolePermiso")]
        public async Task<ActionResult> GetusuariosRolesP(Filtros filtro)
        {
            try
            {
                List<Roles_Permisos> RP = new List<Roles_Permisos>();
                List<Menus> MenuSalida = new List<Menus>();
                var Listado = await usuariosRoles.GetAll(filtro);
                var permisosCodigos = new List<string>();

                foreach(var r in Listado)
                {
                    var rolesPermiso = await dbContext.Roles_Permisos.
                                            Where(s => s.codigo_rol == r.codigo_rol)
                                             .Select(s => s.codigo_permiso).
                                            ToListAsync();
                    permisosCodigos.AddRange(rolesPermiso);
                }

                permisosCodigos = permisosCodigos.Distinct().ToList();

                var permisos = await dbContext.Permisos.Where(p => permisosCodigos.Contains(p.codigo_permiso)).ToListAsync();

                var menus = await dbContext.Menus
                       .ToListAsync();

                foreach (var menu in menus)
                {
                    if (permisosCodigos.Any(codigo => codigo.Contains(menu.view)))
                    {
                        MenuSalida.Add(menu);
                    }
                }

                var permisosAgrupados = MenuSalida
                    .GroupBy(m => m.view) // Agrupar por la columna VIEW
                    .Select(g => new
                    {
                        View = g.Key,
                        Permisos = g.Select(m => new
                        {
                            m.id,
                            m.titulo,
                            m.icon,
                            m.to,
                            m.estado,
                            m.function,
                            m.view
                        }).ToList()
                    })
                    .ToList();

                return Ok(permisosAgrupados);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
