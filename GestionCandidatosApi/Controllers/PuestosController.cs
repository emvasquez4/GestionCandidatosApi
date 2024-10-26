using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCandidatosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PuestosController : Controller
    {
        public readonly IPuestosService puestos;
        public PuestosController(IPuestosService _puestos)
        {
            puestos = _puestos;
        }
        [HttpPost]
        [Route("GetAllPuestos")]
        public async Task<ActionResult<List<Puestos>>> GetAllPuestos(Filtros filtro)
        {        /***********CONSULTAR***********/
            try
            {
                var ListadoPuestos = await puestos.GetAll(filtro);
                return Ok(ListadoPuestos);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("updatePuesto")]
        public async Task<ActionResult<int>> UpdatePuesto(Puestos modelo)
        {           /**********ACTUALIZAR**********/
            try
            {
                var result = await puestos.UpdatePuestos(modelo);

                // Validamos el resultado y devolvemos el código HTTP correspondiente
                switch (result)
                {
                    case 0:
                        return Ok("Puesto actualizado correctamente.");
                    case 1:
                        return NotFound("Puesto no encontrado.");
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
        [Route("addPuesto")]
        public async Task<ActionResult<string>> InsertPuesto([FromBody] Puestos modelo)
        {           /***********AGREGAR************/
            try
            {
                var result = await puestos.InsertPuestos(modelo);

                if (result == "Exito")
                {
                    return Ok("Puesto insertado correctamente.");
                }
                else
                {
                    return BadRequest("Hubo un problema al insertar el Puesto.");
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y devolvemos un BadRequest con el mensaje de error
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("DeletePuesto")]
        public async Task<ActionResult<string>> DeletePuesto(Puestos modelo)
        {           /***********ELIMINAR************/
            try
            {
                // Llama al servicio para eliminar el candidato
                var result = await puestos.DeletePuesto(modelo);

                if (result == "Exito")
                {
                    return Ok("Puesto eliminado correctamente.");
                }
                else
                {
                    return NotFound("Puesto no encontrado.");
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
