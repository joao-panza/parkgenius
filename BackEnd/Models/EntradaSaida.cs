using System.ComponentModel.DataAnnotations;

namespace ParkGenius.Models
{
    public class EntradaSaida
    {
        [Key]
        public int id_Registro { get; set; }
        public required int Id_Veiculo { get; set; } 
        public required int matricula_funcionario { get; set; }
        public DateTime data_entrada { get; set; }
        public DateTime? data_saida { get; set; }
        public TimeSpan Tempo_Total { get; set; } 
        public float Valor_Pago { get; set; } 
        public bool Desconto_Aplicado { get; set; }

    }
}
