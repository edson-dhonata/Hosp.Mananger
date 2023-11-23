using HospManager.Domain.Entities;
using HospMananger.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace HospMananger.Data.Data.ORM
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<EstadoPaciente> EstadoPaciente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Pega todas as proprierdades de modelo cujo o tipo seja string
            var listaPropriedades = modelBuilder.Model.GetEntityTypes()
                                                      .SelectMany(x => x.GetProperties().Where(x => x.ClrType == typeof(string)));


            //Percorre essas propriedades
            foreach (var property in listaPropriedades)
            {
                //Define elas para varchar 90 ao invez delas ficarem com tamanho varchar(max) que vem como padrão.
                property.SetColumnType("varchar(90)");
            }

            //Aplica configurações feitas nas classes mapeadas de forma manual.
            //modelBuilder.ApplyConfiguration(new EstadoPacienteMap());
            //modelBuilder.ApplyConfiguration(new PacienteMap());

            //Aplica configuração de mapeamento para todas as entidades que fazem parte do contexto via assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            //Pega todos os relacionamentos dos modelos.
            var listaRelacionamentos = modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys());

            foreach (var relacionamento in listaRelacionamentos)
            {
                relacionamento.DeleteBehavior = DeleteBehavior.ClientSetNull; // Seta nulo para cascata quando deletar elementos relacionados.
            }

            base.OnModelCreating(modelBuilder); 
        }
    }
}
