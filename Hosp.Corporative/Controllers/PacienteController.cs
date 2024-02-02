using Hosp.Corporative.Extensions.ViewComponents.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Hosp.Corporative.Controllers
{
    //Posso ter mais de uma rota para meu controlador.
    [Route("")]
    [Route("gestao-de-paciente")]
    [Route("gestao-de-pacientes")]
    public class PacienteController : BaseController
    {

        //Não usar rota e method ao mesmo tempo.
        //[HttpGet("")]
        [Route("pacientes")]
        [Route("obter-pacientes")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("detalhe-de-paciente/{id}")]
        public IActionResult DetalheDePaciente(string id)
        {
            return View();
        }

    }
}
