using HospManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospMananger.Data.Data.ORM
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<EstadoPaciente> EstadoPaciente { get; set; }
    }
}
