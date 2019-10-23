using System;
using SC.SDK.NetStandard.DomainCore.Events;

namespace Localiza.LocalRental.Domain.Events
{
    public class VeiculoDevolvido : Event
    {
        public string VeiculoId { get; }
        public string AluguelId { get; }
        public DateTime DataHoraDevolucao { get; }

        public VeiculoDevolvido(string veiculoId, string aluguelId, DateTime dataHoraDevolucao)
        {
            VeiculoId = veiculoId;
            AluguelId = aluguelId;
            DataHoraDevolucao = dataHoraDevolucao;
        }
    }
}
