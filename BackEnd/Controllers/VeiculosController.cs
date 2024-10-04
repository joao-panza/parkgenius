using Microsoft.AspNetCore.Mvc;
using ParkGenius.Data;
using ParkGenius.Models;

namespace ParkGenius.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly DataContext _context;
        public VeiculosController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult CadastrarVeiculo([FromBody] VeiculoDto veiculo)
        {
            // Verifica se o modelo recebido é válido
            if (ModelState.IsValid)
            {
                // Converte o DTO em uma entidade de funcionário
                var novoVeiculo = new Veiculo
                {
                    Marca = veiculo.Marca,
                    Cor = veiculo.Cor,
                    Placa = veiculo.Placa
                    
                };

                // Adiciona o funcionário ao contexto
                _context.Veiculos.Add(novoVeiculo);

                // Salva as alterações no banco de dados
                _context.SaveChanges();


                var NovaEntrada = new EntradaSaida
                {
                    Id_Veiculo = novoVeiculo.Id_Veiculo,
                    matricula_funcionario = 1,
                    data_entrada = DateTime.Parse(veiculo.dataHoraEntrada),
                    data_saida = null,
                    Desconto_Aplicado = false
                };

                _context.EntradasSaidas.Add(NovaEntrada);

                _context.SaveChanges();

                // Retorna uma resposta de sucesso
                return Ok(new { message = "Veiculo e entrada registrados com sucesso!" });
            }

            // Se o modelo for inválido, retorna uma resposta de erro
            return BadRequest("Erro ao registrar veículo e entrada:.");
        }

        [HttpGet]
        public IActionResult GetVeiculosEstacionados()
        {
            var veiculos = (from entrada in _context.EntradasSaidas
                            join veiculo in _context.Veiculos
                            on entrada.Id_Veiculo equals veiculo.Id_Veiculo
                            where entrada.data_saida == null // Veículos ainda estacionados
                            select new
                            {
                                veiculo.Placa,               // Acessa a placa do veículo
                                veiculo.Marca,
                                veiculo.Cor,
                                entrada.data_entrada,          // Acessa a data de entrada
                                entrada.data_saida,
                                veiculo.Id_Veiculo
                            })
                            .ToList();

            return Ok(veiculos);
        }

        [HttpPut("{id}/saida")]
        public IActionResult AtualizarSaida(int id, DateTime registrarSaida)
        {
            var entradaSaida = _context.EntradasSaidas.FirstOrDefault(e => e.Id_Veiculo == id 
            && e.data_saida == null);

            if(entradaSaida == null)
            {
                return NotFound("Veiculo não encontrado ou já saiu.");
            }

            entradaSaida.data_saida = registrarSaida.Date;

            _context.SaveChanges();

            return Ok("Saída Registrada com Sucesso");
        }
    }
}
