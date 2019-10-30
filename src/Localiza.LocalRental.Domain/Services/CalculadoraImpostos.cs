using System;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Services
{
    public class CalculadoraImpostos : DomainService, ICalculadoraImpostos
    {
        private const decimal ALIQUOTA = 3M;

        public decimal CalcularValorImpostos(decimal value)
        {
            return value * ALIQUOTA / 100;
        }
    }

    public interface ICalculadoraImpostos
    {
        decimal CalcularValorImpostos(decimal value);
    }
}
