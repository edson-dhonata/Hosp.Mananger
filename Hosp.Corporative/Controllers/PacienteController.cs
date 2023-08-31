using Microsoft.AspNetCore.Mvc;

namespace Hosp.Corporative.Controllers
{
    //Posso ter mais de uma rota para meu controlador.
    [Route("")]
    [Route("gestao-de-paciente")]
    [Route("gestao-de-pacientes")]
    public class PacienteController : BaseController
    {

        //Não usar rota e method ao mesmo tempo.
        //[HttpGet("")]
        [Route("pacientes")]
        [Route("obter-pacientes")]
        public IActionResult Index()
        {
            var pacientes = ObterPacientes();

            return View(pacientes);
        }
        
        [Route("detalhe-de-paciente/{id}")]
        public IActionResult DetalheDePaciente(string id)
        {
            return View();
        }

        //[Route("adicionar-paciente")]
        [HttpPost("adicionar-paciente")]
        public IActionResult AdicionarPaciente()
        {
            return View();
        }

        #region: Lista de Pacientes
        private List<Paciente> ObterPacientes() 
        {

            var pacientes = new List<Paciente>()
            {
                new Paciente
                {
                    Nome = "Ricardo de Souza",
                    Cpf = "14725836923",
                    Telefones = new List<Telefone>()
                    {
                        new Telefone { Id = Guid.NewGuid(), Numero = "11999632587", TipoDeTelefone = "Residencial"},
                        new Telefone { Id = Guid.NewGuid(), Numero = "11978451256", TipoDeTelefone = "Celular"},
                        new Telefone { Id = Guid.NewGuid(), Numero = "11895623127", TipoDeTelefone = "Celular"}
                    }
                },
                new Paciente
                {
                    Nome = "Jose Mariano",
                    Cpf = "44179816854",
                    Telefones = new List<Telefone>()
                    {
                        new Telefone { Id = Guid.NewGuid(), Numero = "88987546325", TipoDeTelefone = "Residencial"},
                        new Telefone { Id = Guid.NewGuid(), Numero = "21985632514", TipoDeTelefone = "Celular"},
                        new Telefone { Id = Guid.NewGuid(), Numero = "11987521496", TipoDeTelefone = "Celular"}
                    }
                },
                new Paciente
                {
                    Nome = "Maria de Oliveira",
                    Cpf = "11880618602",
                    Telefones = new List<Telefone>()
                    {
                        new Telefone { Id = Guid.NewGuid(), Numero = "11999078574", TipoDeTelefone = "Residencial"},
                        new Telefone { Id = Guid.NewGuid(), Numero = "11653296587", TipoDeTelefone = "Celular"},
                        new Telefone { Id = Guid.NewGuid(), Numero = "24987542136", TipoDeTelefone = "Celular"}
                    }
                }
            };

            return pacientes;
        }
        #endregion

    }
    public class Paciente
    {
        public Paciente()
        {
            Id = Guid.NewGuid();

            //HashSet garante que cada elemento na coleção seja único. Isso significa que não pode haver duplicatas de valores na coleção.
            //Se você tentar adicionar o mesmo elemento duas vezes, apenas uma cópia será mantida no conjunto.
            Telefones = new HashSet<Telefone>();
        }

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public ICollection<Telefone> Telefones { get; set; }
    }

    public class Telefone
    {
        public Guid Id { get; set; }
        public string? TipoDeTelefone { get; set; }
        public string? Numero { get; set; }
    }
}
