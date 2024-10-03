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
                // Converte o DTO em uma entidade de funcionário
                var novoFuncionario = new Funcionario
                {
                    Nome = funcionario.Nome,
                    //Matricula = funcionario.Matricula,
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

            // Se o modelo for inválido, retorna uma resposta de erro
            return BadRequest("Erro ao cadastrar funcionário.");
        }
    }
}
