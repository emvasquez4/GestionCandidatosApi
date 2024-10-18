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
    }
}
