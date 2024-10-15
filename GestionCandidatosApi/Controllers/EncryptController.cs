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
    public class EncryptController : Controller
    {
        private readonly EncryptionService _encryptionService;
        public readonly Utilidades _utilidades;

        public EncryptController( EncryptionService encryptionService = null)
        {
            _encryptionService = encryptionService;
        }

        [HttpPost]
        [Route("Encrypt")]
        public async Task<ActionResult<string>> Encriptar(string texto)
        {
            try
            {
               
                var textEncrypted = _encryptionService.Encrypt(texto);
                return Ok(textEncrypted);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        [HttpPost]
        [Route("Decrypt")]
        public async Task<ActionResult<string>> Descriptar(string texto)
        {

            try
            {

                var textEncrypted = _encryptionService.Decrypt(texto);
                return Ok(textEncrypted);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
