using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ParkGenius
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Adicione o CORS aqui
            services.AddCors(options =>
            {
                options.AddPolicy("PermitirTudo", builder =>
                {
                    builder.AllowAnyOrigin()    // Permite qualquer origem
                           .AllowAnyMethod()    // Permite qualquer método HTTP (GET, POST, etc.)
                           .AllowAnyHeader();   // Permite qualquer cabeçalho
                });
            });
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
