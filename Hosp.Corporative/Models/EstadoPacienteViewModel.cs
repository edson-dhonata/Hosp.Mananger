using HospManager.Domain.Entities;

namespace Hosp.Corporative.Models
{
    public class EstadoPacienteViewModel
    {
        public Guid Id { get; set; }
        public string? Descricao { get; set; }
        public virtual ICollection<Paciente>? Pacientes { get; set; }
    }
}
