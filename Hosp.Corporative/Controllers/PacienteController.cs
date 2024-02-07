using Hosp.Corporative.Extensions.ViewComponents.Helpers;
using Hosp.Corporative.Models;
using HospMananger.Data.Data.ORM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hosp.Corporative.Controllers
{
    public class PacienteController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public PacienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await GetListPacientes());
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id) 
        {
            return View(await GetPaciente(id));       
        }

        #region Métodos para controllers
        private async Task<List<PacienteViewModel>> GetListPacientes()
        {
            var pacientes = await _context.Paciente
                                                  .Include(x => x.EstadoPaciente)
                                                  .AsNoTracking() // Não rastrear em memória.
                                                  .ToListAsync();

            List<PacienteViewModel> listView = new();

            foreach (var item in pacientes)
            {
                listView.Add(new PacienteViewModel()
                {
                    Ativo = item.Ativo,
                    CPF = item.CPF,
                    DataInternacao = item.DataInternacao,
                    DataNascimento = item.DataNascimento,
                    Email = item.Email,
                    EstadoPaciente = item.EstadoPaciente,
                    EstadoPacienteId = item.EstadoPacienteId,
                    Id = item.Id,
                    Nome = item.Nome,
                    Rg = item.Rg,
                    RgDataEmissao = item.RgDataEmissao,
                    RgOrgao = item.RgOrgao,
                    Sexo = item.Sexo,
                    TipoPaciente = item.TipoPaciente,
                    Motivo = item.Motivo
                });
            }

            return listView;
        }
        private async Task<PacienteViewModel> GetPaciente(Guid id)
        {
            var paciente = await _context.Paciente
                                   .Include(x => x.EstadoPaciente)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(x => x.Id == id);

            return new()
            {
                Ativo = paciente.Ativo,
                CPF = paciente.CPF,
                DataInternacao = paciente.DataInternacao,
                DataNascimento = paciente.DataNascimento,
                Email = paciente.Email,
                EstadoPaciente = paciente.EstadoPaciente,
                EstadoPacienteId = paciente.EstadoPacienteId,
                Id = paciente.Id,
                Nome = paciente.Nome,
                Rg = paciente.Rg,
                RgDataEmissao = paciente.RgDataEmissao,
                RgOrgao = paciente.RgOrgao,
                Sexo = paciente.Sexo,
                TipoPaciente = paciente.TipoPaciente,
                Motivo = paciente.Motivo
            };

        }
        #endregion
    }
}
