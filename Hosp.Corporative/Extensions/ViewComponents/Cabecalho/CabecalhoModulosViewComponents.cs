using Hosp.Corporative.Extensions.ViewComponents.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Hosp.Corporative.Extensions.ViewComponents.Cabecalho
{
    [ViewComponent(Name = "Cabecalho")]
    public class CabecalhoModulosViewComponents : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string titulo, string subTitulo)
        {
            Modulo model = new()
            {
                Titulo = titulo,
                SubTitulo = subTitulo
            };

            return await Task.FromResult(View(model));
        }
    }
}
