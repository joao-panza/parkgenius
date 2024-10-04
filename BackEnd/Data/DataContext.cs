using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkGenius.Models;

namespace ParkGenius.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Funcionario> Funcionarios{ get; set; }
        public DbSet<Veiculo> Veiculos{ get; set; }

        public DbSet<EntradaSaida> EntradasSaidas{ get; set; }

    }
}