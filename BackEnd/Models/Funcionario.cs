using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ParkGenius.Models
{
    public class Funcionario
    {
        [Key]
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
