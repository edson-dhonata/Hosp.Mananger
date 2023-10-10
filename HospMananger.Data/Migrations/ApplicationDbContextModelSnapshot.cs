﻿// <auto-generated />
using System;
using HospMananger.Data.Data.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HospMananger.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HospManager.Domain.Entities.EstadoPaciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EstadoPaciente");
                });

            modelBuilder.Entity("HospManager.Domain.Entities.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<bool>("CPF")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataInternacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EstadoPacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Motivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RgDataEmissao")
                        .HasColumnType("datetime2");

                    b.Property<string>("RgOrgao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<int>("TipoPaciente")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstadoPacienteId");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("HospManager.Domain.Entities.Paciente", b =>
                {
                    b.HasOne("HospManager.Domain.Entities.EstadoPaciente", "EstadoPaciente")
                        .WithMany("Pacientes")
                        .HasForeignKey("EstadoPacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EstadoPaciente");
                });

            modelBuilder.Entity("HospManager.Domain.Entities.EstadoPaciente", b =>
                {
                    b.Navigation("Pacientes");
                });
#pragma warning restore 612, 618
        }
    }
}
