using HospManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospMananger.Data.Mappings
{
	//IEntityTypeConfiguration: Interface para mapear entidades com tabela do banco de dados.
	//Propriedades com campos nas tabelas, relacionamentos etc...
	public class EstadoPacienteMap : IEntityTypeConfiguration<EstadoPaciente>
	{
		public void Configure(EntityTypeBuilder<EstadoPaciente> builder)
		{
			
			//Defini propriedade Id como primary key
			builder.HasKey(x => x.Id);

			builder.Property(p => p.Descricao)
						.HasColumnType("varchar(30)") //Tipo de dado no banco
						.IsRequired() // Campo requerido
						.HasColumnName("Descricao"); // Nome da coluna no banco de dados

			builder.ToTable(nameof(EstadoPaciente)); // Define nome da tabela no banco de dados

			//Relacionamento muitos para um
			builder.HasMany(p => p.Pacientes)
				   .WithOne(p => p.EstadoPaciente);
				   //.OnDelete(DeleteBehavior.Cascade) // Deleta em cascada
				   //.OnDelete(DeleteBehavior.NoAction) // Não deleta em cascada
		}
	}
}
