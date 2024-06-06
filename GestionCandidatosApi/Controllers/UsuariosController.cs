using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditCard.Controllers
{
    public class UsuariosController : Controller
    {
        public readonly IUsuariosService usuarios;

        public UsuariosController(IUsuariosService _usuarios) {
            usuarios = _usuarios;
        }

        [HttpPost]
        [Route("GetAllUsuarios")]
        public async Task<ActionResult<List<Usuarios>>> GetAllUsuarios(Filtros filtro)
        {
            try
            {
               
                var ListadoUsuarios = await usuarios.GetAll(filtro);
                return Ok(ListadoUsuarios = await usuarios.GetAll(filtro));
);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

  
    }
}
