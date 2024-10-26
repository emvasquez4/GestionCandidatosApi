using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using GestionCandidatosApi.Services.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionCandidatosApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class EntrevistasController : Controller
    {
        public readonly IEntrevistasService entrevistas;

        public EntrevistasController(IEntrevistasService _entrevistas)
        {
            entrevistas = _entrevistas;
        }
        [HttpPost]
        [Route("GetAllEntrevistas")]
        public async Task<ActionResult<List<Entrevistas>>> GetAllEntrevistas(Filtros filtro)
        {             /***********CONSULTAR***********/
            try
            {

                var ListadoEntrevistas = await entrevistas.GetAll(filtro);
                return Ok(ListadoEntrevistas);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("updateEntrevista")]
        public async Task<ActionResult<int>> UpdateEntrevista(Entrevistas modelo)
        {           /**********ACTUALIZAR**********/
            try
            {
                var result = await entrevistas.UpdateEntrevistas(modelo);

                // Validamos el resultado y devolvemos el código HTTP correspondiente
                switch (result)
                {
                    case 0:
                        return Ok("Entrevista actualizado correctamente.");
                    case 1:
                        return NotFound("Entrevista no encontrado.");
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
        [Route("addEntrevista")]
        public async Task<ActionResult<string>> InsertEntrevista([FromBody] Entrevistas modelo)
        {           /***********AGREGAR************/
            try
            {
                var result = await entrevistas.InsertEntrevistas(modelo);

                if (result == "Exito")
                {
                    return Ok("Entrevista insertado correctamente.");
                }
                else
                {
                    return BadRequest("Hubo un problema al insertar el Entrevista.");
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y devolvemos un BadRequest con el mensaje de error
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("DeleteEntrevista")]
        public async Task<ActionResult<string>> DeleteEntrevista(Entrevistas modelo)
        {           /***********ELIMINAR************/
            try 
            {
                // Llama al servicio para eliminar el candidato
                var result = await entrevistas.DeleteEntrevista(modelo);

                if (result == "Exito")
                {
                    return Ok("Entrevista eliminado correctamente.");
                }
                else
                {
                    return NotFound("Entrevista no encontrado.");
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
