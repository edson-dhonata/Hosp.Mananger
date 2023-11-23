using HospManager.Domain.Entities;
using HospManager.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Hosp.Corporative.Models
{
    public class EstadoPacienteViewModel : EntityBase
    {
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(30, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres!", MinimumLength = 3)]
        public string? Descricao { get; set; }

        public virtual ICollection<Paciente>? Pacientes { get; set; }
    }
}
