using System;
namespace Localiza.LocalRental.Domain.Services
{
    public interface IParametrosCobranca
    {
        decimal ValorHoraBaseAluguel { get; }
        decimal ValorHoraBaseOpcionais { get; }
    }
}
