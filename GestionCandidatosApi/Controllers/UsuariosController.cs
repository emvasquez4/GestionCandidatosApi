using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using GestionCandidatosApi.Services.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditCard.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        public readonly IUsuariosService usuarios;
        public readonly Utilidades _utilidades;

        public UsuariosController(IUsuariosService _usuarios, Utilidades utilidades) {
            usuarios = _usuarios;
            _utilidades = utilidades;
        }

        [HttpPost]
        [Route("GetAllUsuarios")]
        public async Task<ActionResult<List<Usuarios>>> GetAllUsuarios(Filtros filtro)
        {
            try
            {
               
                var ListadoUsuarios = await usuarios.GetAll(filtro);
                return Ok(ListadoUsuarios);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<List<Usuarios>>> Login(Usuarios user)
        {
            try
            {

                var ListadoUsuarios = await usuarios.GetUsuario(user);

                if(ListadoUsuarios == null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
                }
                else{
                    var token = _utilidades.generarJWT(ListadoUsuarios);
                    return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = token, usuario = ListadoUsuarios });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("updateUsuario")]
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

        [HttpPost]
        [Route("addUsuario")]
        public async Task<ActionResult<string>> InsertUsuario([FromBody]Usuarios modelo)
        {
            try
            {
                var result = await usuarios.InsertUsuarios(modelo);

                if (result == "Exito")
                {
                    return Ok("Usuario insertado correctamente.");
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
