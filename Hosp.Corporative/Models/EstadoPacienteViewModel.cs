using HospManager.Domain.Entities;
using HospManager.Domain.Entities.Base;

namespace Hosp.Corporative.Models
{
    public class EstadoPacienteViewModel : EntityBase
    {
        public string? Descricao { get; set; }
        public virtual ICollection<Paciente>? Pacientes { get; set; }
    }
}
