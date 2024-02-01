using HospMananger.Data.Data.ORM;
using Microsoft.EntityFrameworkCore;

namespace Hosp.Corporative.Extensions.ViewComponents.Helpers
{
    public static class Util
    {
        public static int TotReg(ApplicationDbContext ctx) => ctx.Paciente.AsNoTracking().Count();

        public static decimal GetNumRegEstado(ApplicationDbContext ctx, string estado) => ctx.Paciente.AsNoTracking().Count(x => x.EstadoPaciente.Descricao.Contains(estado));

    }
}
