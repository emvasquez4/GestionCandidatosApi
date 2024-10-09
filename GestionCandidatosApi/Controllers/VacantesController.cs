using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionCandidatosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacantesController : ControllerBase
    {
        private readonly IVacantesService _vacantesService;

        public VacantesController(IVacantesService vacantesService)
        {
            _vacantesService = vacantesService;
        }

        // Método HTTP POST que maneja solicitudes para obtener todas las vacantes, filtradas según los parámetros recibidos
        [HttpPost]
        [Route("GetAllVacantes")]
        public async Task<ActionResult<List<Vacantes>>> GetAllVacantes(Filtros filtro)
        {
            try
            {
                var listadoVacantes = await _vacantesService.GetAll(filtro);
                return Ok(listadoVacantes);
            }
            catch (Exception ex)
            {
                // Aquí puedes agregar un logger para capturar la excepción
                return BadRequest(ex.Message);
            }
        }

        // Método HTTP POST que maneja solicitudes para actualizar una vacante
        [HttpPost]
        [Route("UpdateVacante")]
        public async Task<ActionResult<int>> UpdateVacante(Vacantes modelo)
        {
            try
            {
                // Llamamos al método UpdateVacantes del servicio
                var result = await _vacantesService.UpdateVacantes(modelo);

                // Validamos el resultado y devolvemos el código HTTP correspondiente
                switch (result)
                {
                    case 0:
                        return Ok("Vacante actualizada correctamente.");
                    case 1:
                        return NotFound("Vacante no encontrada.");
                    case 2:
                        return BadRequest("La nueva información no puede ser igual a la actual o anterior.");
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
