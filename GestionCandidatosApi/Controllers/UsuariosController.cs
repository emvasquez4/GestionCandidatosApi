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

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateUsuario")]
        public async Task<ActionResult<int>> UpdateUsuario(Usuarios modelo)
        {
            try
            {
                // Llamamos al método UpdateUsuarios del servicio
                var result = await usuarios.UpdateUsuarios(modelo);

                // Validamos el resultado y devolvemos el código HTTP correspondiente
                switch (result)
                {
                    case 0:
                        return Ok("Usuario actualizado correctamente.");
                    case 1:
                        return NotFound("Usuario no encontrado.");
                    case 2:
                        return BadRequest("La nueva contraseña no puede ser igual a la contraseña actual o la anterior.");
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


    }
}
