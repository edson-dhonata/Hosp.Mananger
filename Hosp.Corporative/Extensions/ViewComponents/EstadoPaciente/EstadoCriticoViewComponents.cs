using Microsoft.AspNetCore.Mvc;

namespace Hosp.Corporative.Extensions.ViewComponents.EstadoPaciente
{
    //Data annotation para nomear a view component
    [ViewComponent(Name = "EstadoCritico")]
    public class EstadoCriticoViewComponents : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
