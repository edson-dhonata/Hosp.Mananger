using Hosp.Corporative.Extensions.ViewComponents.Helpers;
using Hosp.Corporative.Models;
using HospManager.Domain.Entities;
using HospMananger.Data.Data.ORM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View(await MapperListOfModelsToListOfViewModels(await ObterTodos()));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id) 
        {
            return View(await GetPaciente(id));       
        }

        [HttpGet]
        public async Task<IActionResult> ObterPacientesPorEstadopaciente(Guid estadoPacienteId)
        {
            return View(await MapperListOfModelsToListOfViewModels(await ObterTodosPorEPaciente(estadoPacienteId)));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.EstadoPaciente = new SelectList(await ListaEstadoPaciente(), "Id", "Descricao");
            return await Task.FromResult(View());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PacienteViewModel pacienteViewModel)
        {
            if (ModelState.IsValid)
            {
                    Paciente paciente = MapperOfTheViewModelToModel(pacienteViewModel);

                try
                {
                    _context.Set<Paciente>().Add(paciente);
                    await _context.SaveChangesAsync();
                    TempData["Sucesso"] = "Registro Cadastrado com Sucesso!";
                    return Redirect(nameof(Index));

                }
                catch (Exception)
                {
                    return View(pacienteViewModel);
                }
            }

            ViewBag.EstadoPaciente = new SelectList(await ListaEstadoPaciente(), "Id", "Descricao");
            return View(pacienteViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Paciente? paciente = await _context.Set<Paciente>()
                .Include(x => x.EstadoPaciente)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (paciente is null)
            {
                return NotFound();
            }

            ViewBag.EstadoPaciente = new SelectList(await ListaEstadoPaciente(), "Id", "Descricao");

            return View(await MapperOfModelToViewModel(paciente));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PacienteViewModel pacienteVM)
        {
            if (id != pacienteVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var paciente = MapperOfTheViewModelToModel(pacienteVM);
                    _context.Entry(paciente).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    TempData["Sucesso"] = "Registro Atualizado com Sucesso!";
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException dbConflitex) //Exeção ao tentar atualizar o mesmo modelo ao mesmo tempo.
                {
                    if (!TemPaciente(pacienteVM.Id))
                    {
                        return BadRequest(dbConflitex.Message); // O código de status de resposta HTTP 400 Bad Request indica que o servidor não pode ou não
                                                                // irá processar a requisição devido a alguma coisa que foi entendida como um erro do cliente
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message); // O código de status de resposta HTTP 400 Bad Request indica que o servidor não pode ou não
                                                   // irá processar a requisição devido a alguma coisa que foi entendida como um erro do cliente
                }
            }

            ViewBag.EstadoPaciente = new SelectList(await ListaEstadoPaciente(), "Id", "Descricao");
            return View(pacienteVM);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            Paciente? paciente = await _context.Set<Paciente>()
                .Include(x => x.EstadoPaciente)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (paciente is null)
            {
                return NotFound();
            }

            return View(await MapperOfModelToViewModel(paciente));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Paciente? paciente = await _context.Set<Paciente>()
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (paciente is null)
            {
                TempData["Error"] = $"Registro com ID: {id}, não foi encontrado!";
                return NotFound();
            }

            _context.Entry(paciente).State = EntityState.Deleted; //Muda estado do objeto para deletado.
            await _context.SaveChangesAsync();
            TempData["Sucesso"] = "Registro Deletado com Sucesso!";

            return Redirect(nameof(Index));
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
                    CPF = item.CPF ?? string.Empty,
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
                Ativo = paciente?.Ativo ?? false,
                CPF = paciente?.CPF ?? string.Empty,
                DataInternacao = paciente?.DataInternacao ?? null,
                DataNascimento = paciente?.DataNascimento ?? null,
                Email = paciente?.Email,
                EstadoPaciente = paciente?.EstadoPaciente,
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

        #region: Retorna uma lista de todos os Pacientes cadastrados
        private async Task<List<Paciente>> ObterTodos()
        {
            return await _context.Paciente
                .Include(x => x.EstadoPaciente)
                .AsNoTracking().ToListAsync();
        }
        #endregion

        #region: Retorna um Paciente cadastrado, filtrado pelo seu Id
        private async Task<Paciente> ObterPorId(Guid id)
        {
            var paciente = await _context.Paciente
                .Include(x => x.EstadoPaciente)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return await Task.FromResult(paciente ?? new Paciente());

        }
        #endregion

        #region: Mapper de List<Model> para List<ViewModel>
        private async Task<List<PacienteViewModel>> MapperListOfModelsToListOfViewModels(List<Paciente>? pacientes)
        {
            List<PacienteViewModel> listView = new();

            foreach (var item in pacientes)
            {
                listView.Add(new PacienteViewModel
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

            return await Task.FromResult(listView);
        }
        #endregion

        #region: Retorna um booleano indicando se tem paciente Paciente

        private bool TemPaciente(Guid pacienteId)
        {
            return _context.Paciente.Any(x => x.Id == pacienteId);
        }

        #endregion

        #region: Obtem todos os Paciente, filtrado por um único EstadoPaciente
        private async Task<List<Paciente>> ObterTodosPorEPaciente(Guid estadoPacienteId)
        {
            return await _context.Paciente
                .Include(ep => ep.EstadoPaciente)
                .AsNoTracking()
                .Where(x => x.EstadoPaciente.Id == estadoPacienteId)
                .OrderBy(order => order.Nome)
                .ToListAsync();
        }
        #endregion

        #region: Mapper de Model para ViewModel
        private async Task<PacienteViewModel> MapperOfModelToViewModel(Paciente? item)
        {
            PacienteViewModel viewModel = new()
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
            };

            return await Task.FromResult(viewModel);

        }
        #endregion

        #region: Mapper Of The ViewModel To Model
        private static Paciente MapperOfTheViewModelToModel(PacienteViewModel pacienteViewModel)
        {
            var paciente = new Paciente()
            {
                //Id = Guid.NewGuid(), // Herda de EntityBase

                Ativo = pacienteViewModel.Ativo,
                CPF = pacienteViewModel.CPF,
                DataInternacao = (DateTime)pacienteViewModel.DataInternacao,
                DataNascimento = (DateTime)pacienteViewModel.DataNascimento,
                Email = pacienteViewModel.Email,
                EstadoPaciente = pacienteViewModel.EstadoPaciente,
                EstadoPacienteId = pacienteViewModel.EstadoPacienteId,
                Nome = pacienteViewModel.Nome,
                Rg = pacienteViewModel.Rg,
                RgDataEmissao = pacienteViewModel.RgDataEmissao,
                RgOrgao = pacienteViewModel.RgOrgao,
                Sexo = pacienteViewModel.Sexo,
                TipoPaciente = pacienteViewModel.TipoPaciente,
                Motivo = pacienteViewModel.Motivo
            };
            return paciente;
        }
        #endregion

        #region: Retorna uma lista de todos os EstadosDePacientes cadastrados
        private async Task<List<EstadoPaciente>> ListaEstadoPaciente()
        {
            return await _context.EstadoPaciente
                .AsNoTracking().ToListAsync();
        }
        #endregion
    }
}
