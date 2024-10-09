using GestionCandidatosApi.Services;
using Microsoft.AspNetCore.Mvc;
using GestionCandidatosApi.Modelos;
using Microsoft.AspNetCore.Http;

namespace GestionCandidatosApi.Controllers
{
    public class CandidatosController : Controller
    {
        public readonly ICandidatosService candidatos;

        public CandidatosController(ICandidatosService _candidatos)
        {
            candidatos = _candidatos;
        }

    }
}
