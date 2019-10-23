using System;
using SC.SDK.NetStandard.DomainCore.Events;

namespace Localiza.LocalRental.Domain.Events
{
    public class VeiculoRetirado : Event
    {
        public string VeiculoId { get; }
        public string AluguelId { get; }
        public DateTime DataHoraRetirada { get; }

        public VeiculoRetirado(string veiculoId, string aluguelId, DateTime dataHoraRetirada)
        {
            AluguelId = aluguelId;
            VeiculoId = veiculoId;
            DataHoraRetirada = dataHoraRetirada;
        }
    }
}
