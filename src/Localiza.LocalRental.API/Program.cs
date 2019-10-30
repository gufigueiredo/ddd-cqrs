using System;
using System.Diagnostics;
using System.IO;
using Localiza.LocalRental.Infrastructure.DataAccess;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Localiza.LocalRental.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    //var dbPath = $"{Directory.GetCurrentDirectory()}/database.db";
                    //if (File.Exists(dbPath))
                    //    File.Delete(dbPath);
                    //var context = services.GetRequiredService<ILiteDbContext>();
                    //DbSeedData.Seed(context);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex, "ApplicationStartup: Ocorreu um erro ao alimentar o DB.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
