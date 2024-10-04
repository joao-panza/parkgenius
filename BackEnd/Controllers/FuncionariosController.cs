using Microsoft.AspNetCore.Mvc;
using ParkGenius.Data;
using ParkGenius.Models;

namespace ParkGenius.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly DataContext _context;

        public FuncionariosController(DataContext context)
        {
            _context = context;
        }

        // POST: api/funcionarios
        [HttpPost]
        public IActionResult CadastrarFuncionario([FromBody] Funcionario funcionario)
        {
            // Verifica se o modelo recebido é válido
            if (ModelState.IsValid)
            {
                // Verifica se o login já existe no banco de dados
                var loginExistente = _context.Funcionarios.FirstOrDefault(f => f.Login == funcionario.Login);

                if (loginExistente != null)
                {
                    // Retorna uma resposta de erro informando que o login já está em uso
                    return BadRequest(new { message = "Este login já está em uso. Por favor, escolha outro." });
                }

                // Converte o DTO em uma entidade de funcionário
                var novoFuncionario = new Funcionario
                {
                    Nome = funcionario.Nome,
                    // Matricula = funcionario.Matricula,
                    Telefone = funcionario.Telefone,
                    Email = funcionario.Email,
                    Login = funcionario.Login,
                    Senha = funcionario.Senha
                };

                // Adiciona o funcionário ao contexto
                _context.Funcionarios.Add(novoFuncionario);

                // Salva as alterações no banco de dados
                _context.SaveChanges();

                // Retorna uma resposta de sucesso
                return Ok(new { message = "Funcionário cadastrado com sucesso!" });
            }

            // Se o modelo não for válido, retorna um erro de validação
            return BadRequest(ModelState);
        }


        [HttpGet]
        public IEnumerable<Funcionario> ListarFuncionarios()
        {
            return _context.Funcionarios;
        }
    }
}
