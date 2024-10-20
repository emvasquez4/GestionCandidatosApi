using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCandidatosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenusController : Controller
    {
        public readonly IMenusService Menus;

        public MenusController(IMenusService _Menus)
        {
            Menus = _Menus;
        }

        [HttpPost]
        [Route("GetAllMenus")]
        public async Task<ActionResult<List<Menus>>> GetAllMenus(Filtros filtro)
        {
            try
            {

                var ListadoMenus = await Menus.GetAll(filtro);
                return Ok(ListadoMenus);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addMenu")]
        public async Task<ActionResult<string>> InsertUsuario([FromBody] Menus modelo)
        {
            try
            {
                var result = await Menus.InsertMenus(modelo);

                if (result == "Exito")
                {
                    return Ok("Menu insertado correctamente.");
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
