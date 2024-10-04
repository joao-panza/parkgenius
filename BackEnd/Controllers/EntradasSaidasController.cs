using Microsoft.AspNetCore.Mvc;
using ParkGenius.Data;
using ParkGenius.Models; // Supondo que a entidade Veiculo esteja nesse namespace
using System.Linq;

namespace ParkGenius.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntradasSaidasController : Controller
    {
        private readonly DataContext _context;

        public EntradasSaidasController(DataContext context)
        {
            _context = context;
        }

        // Método para buscar veículos com base na placa
        [HttpGet("BuscarVeiculoPorPlaca/{placa}")]
        public IActionResult BuscarVeiculos(string placa)
        {
            // Verifica se a placa foi informada
            if (string.IsNullOrEmpty(placa))
            {
                return BadRequest(new { message = "A placa não pode estar vazia." });
            }

            // Busca veículos que contenham a placa fornecida
            var EntradaSaida = (from entrada in _context.EntradasSaidas
                                join veiculo in _context.Veiculos
                                on entrada.Id_Veiculo equals veiculo.Id_Veiculo
                                where entrada.data_saida == null // Veículos ainda estacionados
                                select new
                                {
                                    veiculo.Placa,
                                    veiculo.Cor,
                                    veiculo.Marca
                                }).ToList();

            // Verifica se algum veículo foi encontrado
            if (EntradaSaida == null)
            {
                return NotFound(new { message = "Nenhum veículo encontrado com essa placa." });
            }

            // Retorna a lista de veículos encontrados
            return Ok(EntradaSaida);
        }
    }
}
