using HospManager.Domain.Entities;
using HospManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hosp.Corporative.Models
{
    public class PacienteViewModel
    {
        [Key]
        public Guid Id { get; set; }


        public Guid EstadoPacienteId { get; set; }
        public virtual EstadoPaciente? EstadoPaciente { get; set; }

        public string? Nome { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo {0} está inválido.")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Data de Internação")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo {0} está inválido.")] //O primeiro parâmetro e a propriedade.
        public DateTime DataInternacao { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; }
        public bool CPF { get; set; }
        public TipoPaciente TipoPaciente { get; set; }
        public Sexo Sexo { get; set; }
        public string? Rg { get; set; }
        public string? RgOrgao { get; set; }

        [Display(Name = "Data de Emissão RG")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo {0} está inválido.")]
        public DateTime RgDataEmissao { get; set; }
        public string? Motivo { get; set; }

        public override string ToString()
        {
            return Id.ToString() + " - " + Nome;
        }
    }
}
