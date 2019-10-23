using System;
using Localiza.LocalRental.Domain.Services;

namespace Localiza.LocalRental.API.Infrastructure
{
    public class ParametrosCobrancaConfigs : IParametrosCobranca
    {
        public ParametrosCobrancaConfigs(decimal valorHoraBaseAluguel, decimal valorHoraBaseOpcionais)
        {
            ValorHoraBaseAluguel = valorHoraBaseAluguel;
            ValorHoraBaseOpcionais = valorHoraBaseOpcionais;
        }

        public decimal ValorHoraBaseAluguel { get; }

        public decimal ValorHoraBaseOpcionais { get; }
    }
}
