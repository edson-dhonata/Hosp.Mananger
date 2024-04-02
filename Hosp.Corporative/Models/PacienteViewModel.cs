using HospManager.Domain.Entities;
using HospManager.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hosp.Corporative.Models
{
    public class PacienteViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid EstadoPacienteId { get; set; }

        [ForeignKey("EstadoPaciente")]
        [Display(Name = "Estado do Paciente")]
        public virtual EstadoPaciente? EstadoPaciente { get; set; }

        [DisplayName(displayName: "Nome do Paciente")]
        [StringLength(maximumLength: 80, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string? Nome { get; set; }

        [DisplayName(displayName: "Data de Nascimento")]
        [Required(ErrorMessage = "Campo {0} é requerido.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data Inválida.")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Data de Internação")]
        [Required(ErrorMessage = "Campo {0} é requerido.")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo {0} está inválido.")] //O primeiro parâmetro e a propriedade.
        public DateTime? DataInternacao { get; set; }

        //[DisplayName(displayName: "Data Inclusão")]
        //public DateTime? DataInclusao { get; set; }

        //[DisplayName(displayName: "Data Última Modificação")]
        //public DateTime? DataUltimaModificacao { get; set; }

        //[DisplayName(displayName: "Usuário Inclusão")]
        //public string? UsuarioInclusao { get; set; }

        //[DisplayName(displayName: "Usuário Última Modificação")]
        //public string? UsuarioUltimaModificacao { get; set; }

        [DisplayName(displayName: "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Inválido.")]
        public string? Email { get; set; }

        public bool Ativo { get; set; }

        [DisplayName(displayName: "CPF")]
        [Required(ErrorMessage = "Campo {0} é requerido.")]
        [StringLength(11, ErrorMessage = "Campo {0} tem de ter {1} caracteres", MinimumLength = 11)]
        public string? CPF { get; set; }

        [DisplayName(displayName: "Tipo de Paciente")]
        [Required(ErrorMessage = "O campo {0} é requerido.")]
        public TipoPaciente TipoPaciente { get; set; }

        [DisplayName(displayName: "Sexo")]
        [Required(ErrorMessage = "O campo {0} é requerido.")]
        public Sexo Sexo { get; set; }

        [DisplayName(displayName: "RG")]
        [MaxLength(15, ErrorMessage = "O campo {0} não pode ter mais que (1) caracteres.")]
        public string? Rg { get; set; }

        [DisplayName(displayName: "Org.Expedidor")]
        [MaxLength(10, ErrorMessage = "O campo {0} não pode ter mais que (1) caracteres.")]
        public string? RgOrgao { get; set; }

        [DisplayName(displayName: "Data Emissão")]
        [Required(ErrorMessage = "Campo {0} é requerido.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data Inválida.")]
        public DateTime RgDataEmissao { get; set; }

        [MaxLength(90, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string? Motivo { get; set; }

        public override string ToString()
        {
            return Id.ToString() + " - " + Nome;
        }
    }
}
