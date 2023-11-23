using HospManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospMananger.Data.Mappings
{
	public class PacienteMap : IEntityTypeConfiguration<Paciente>
	{
		public void Configure(EntityTypeBuilder<Paciente> builder)
		{
			builder.HasKey(i => i.Id);

			builder.Property(n => n.Nome)
				   .IsRequired()
				   .HasColumnType("varchar(80)")
				   .HasColumnName("Nome");

			builder.Property(e => e.Email)
				   .HasColumnName("Email")
				   .HasColumnType("varchar(150)");

			builder.Property(c => c.CPF)
				   .HasMaxLength(11).IsFixedLength(true) // Define tamanho da propriedade e fixa o tamanho para 11
				   .HasColumnName("CPF")
				   .HasColumnType("varchar(11)");

			builder.Property(r => r.Rg)
				   .HasMaxLength(15)
				   .HasColumnName("Rg")
				   .HasColumnType("varchar(15)");

			builder.Property(o => o.RgOrgao)
				   .HasColumnName("RgOrgao")
				   .HasColumnType("varchar(10)");

			builder.Property(o => o.Motivo)
				   .HasColumnName("Motivo")
				   .HasColumnType("varchar(90)");

			//1:1 => Paciente : EstadoPaciente ou 0:N => EstadoPaciente : Paciente
			builder.HasOne(ep => ep.EstadoPaciente) // 1:1 Paciente : EstadoPaciente
				   .WithMany(p => p.Pacientes)  // 1:N EstadoPaciente : Pacientes
				   .HasForeignKey(fk => fk.EstadoPacienteId) // Fk Estado paciente
				   .HasPrincipalKey(pk => pk.Id); // Pk paciente

			builder.ToTable(nameof(Paciente)); //Nome da tabela no banco, com nameof mostra entidade que representa esta tabela.
		}
	}
}
