using Hosp.Corporative.Extensions.ViewComponents.Helpers;
using HospMananger.Data.Data.ORM;
using Microsoft.AspNetCore.Mvc;

namespace Hosp.Corporative.Extensions.ViewComponents.EstadoPaciente
{
    [ViewComponent(Name = "StatusPaciente")]
    public class StatusPacienteViewComponents : ViewComponent
    {

        private readonly ApplicationDbContext _context;

        public StatusPacienteViewComponents(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string estado)
        {
            var totalGeral = Util.TotReg(_context);

            decimal totalEstado = Util.GetNumRegEstado(_context, estado);

            decimal progress = (totalGeral > 0) ? totalEstado * 100 / totalGeral : 0; 

            var prct = progress.ToString("F1"); //Retorna decimal

            ContadorEstadoPaciente model = new()
            {
                Titulo = $"Pacientes {estado}",
                Parcial = (int)totalEstado,
                Percentual = prct,
                Progress = progress,
                ClassContainer = "panel panel-default tile panelClose panelRefresh",
                IconeLg = "l-banknote",
                IconeSm = "fa fa-arrow-circle-o-down s20 mr5 pull-left"
            };

            return await Task.FromResult(View(model)); // Força um retorno de tarefa, pois o método não contém um método await.
        }
    }
}
