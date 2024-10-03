using System.ComponentModel.DataAnnotations;

namespace ParkGenius.Models
{
    public class Veiculo
    {
        [Key]
        public int Id_Veiculo { get; set; }
        public string Placa { get; set; }
        public string Cor {  get; set; }
        public string Marca { get; set; }

    }
}
