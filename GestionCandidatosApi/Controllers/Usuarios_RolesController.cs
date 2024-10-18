using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCandidatosApi.Controllers
{
    public class Usuarios_RolesController : Controller
    {
        public readonly IUsuariosRolesService usuariosRoles;
        public Usuarios_RolesController()
        {
            
        }
    }
}
