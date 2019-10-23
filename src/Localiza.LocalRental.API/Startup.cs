using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Localiza.LocalRental.Domain.Model.Aluguel;
using Localiza.LocalRental.Infrastructure.DataAccess;
using Localiza.LocalRental.Infrastructure.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Swagger;
using Localiza.LocalRental.Domain.Services;
using Localiza.LocalRental.API.Infrastructure;
using Localiza.LocalRental.Domain.Model.Cliente;
using Localiza.LocalRental.Domain.Model.Fatura;
using Localiza.LocalRental.Domain.Model.Veiculo;
using Localiza.LocalRental.Infrastructure.Services;
using System.Reflection;
using Localiza.LocalRental.API.Application.Queries.Aluguel;
using Localiza.LocalRental.API.Application.Queries.Cliente;
using Localiza.LocalRental.API.Application.Queries.Fatura;
using Localiza.LocalRental.API.Application.Queries.Veiculo;

namespace Localiza.LocalRental.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //AutoMapper;
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //LiteDb
            services.AddLiteDb(@"FileName=database.db;Mode=Exclusive");
            services.AddSingleton<ILiteDbContext, LiteDbContext>();

            //Dependency Injection
            services.AddSingleton<IParametrosCobranca>(x =>
                new ParametrosCobrancaConfigs(
                    Configuration.GetSection("ParametrosCobranca").GetValue<decimal>("ValorHoraBaseAluguel"),
                    Configuration.GetSection("ParametrosCobranca").GetValue<decimal>("ValorHoraBaseOpcionais")
            ));
            services.AddSingleton<ICalculadoraImpostos, CalculadoraImpostos>();

            services.AddScoped<IAluguelRepository, AluguelRepository>();
            services.AddScoped<IAluguelQueries, AluguelQueries>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteQueries, ClienteQueries>();
            services.AddScoped<IFaturaRepository, FaturaRepository>();
            services.AddScoped<IFaturaQueries, FaturaQueries>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IVeiculoQueries, VeiculoQueries>();
            services.AddScoped<IGatewayDePagamento, GatewayDePagamento>();
            services.AddScoped<IFaturarAluguelService, FaturarAluguelService>();

            //Mediator
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            //Healthchecks
            services.AddHealthChecks();

            //Swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Localiza.LocalRentalAPI",
                    Description = "API de exemplo do LocalRentalApp",
                    Version = "v1"
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "Localiza.LocalRental.API.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Healthcheck endpoint
            app.UseHealthChecks("/healthcheck");

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Localiza.LocalRental.API v1");
            });

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
