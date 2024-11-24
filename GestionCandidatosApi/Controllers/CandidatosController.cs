using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Mvc;
using GestionCandidatosApi.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using GestionCandidatosApi.Services.Utilidades;

namespace GestionCandidatosApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class CandidatosController : Controller
    {
        public readonly ICandidatosService candidatos;

        public CandidatosController(ICandidatosService _candidatos)
        {
            candidatos = _candidatos;
        }
        [HttpPost]
        [Route("GetAllCandidatos")]
        public async Task<ActionResult<List<Candidatos>>> GetAllCandidatos(Filtros filtro)
        {        /***********CONSULTAR***********/
            try
            {
                var ListadoCandidatos = await candidatos.GetAll(filtro);
                return Ok(ListadoCandidatos);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("updateCandidato")]
        public async Task<ActionResult<int>> UpdateCandidato(Candidatos modelo)
        {           /**********ACTUALIZAR**********/
            try
            {
                var result = await candidatos.UpdateCandidatos(modelo);

                // Validamos el resultado y devolvemos el código HTTP correspondiente
                switch (result)
                {
                    case 0:
                        return Ok("Candidato actualizado correctamente.");
                    case 1:
                        return NotFound("Candidato no encontrado.");
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
        [Route("addCandidato")]
        public async Task<ActionResult<string>> InsertCandidato([FromBody] Candidatos modelo)
        {           /***********AGREGAR************/
            try
            {
                var result = await candidatos.InsertCandidatos(modelo);

                if (result == "Exito")
                {
                    return Ok("Candidato insertado correctamente.");
                }
                else
                {
                    return BadRequest("Hubo un problema al insertar el Candidato.");
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y devolvemos un BadRequest con el mensaje de error
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("DeleteCandidato")]
        public async Task<ActionResult<string>> DeleteCandidato(Candidatos modelo)
        {           /***********ELIMINAR************/
            try
            {
                // Llama al servicio para eliminar el candidato
                var result = await candidatos.DeleteCandidato(modelo);

                if (result == "Exito")
                {
                    return Ok("Candidato eliminado correctamente.");
                }
                else
                {
                    return NotFound("Candidato no encontrado.");
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
