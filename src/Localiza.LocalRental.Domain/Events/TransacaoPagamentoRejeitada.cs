using System;
using SC.SDK.NetStandard.DomainCore.Events;

namespace Localiza.LocalRental.Domain.Events
{
    public class TransacaoPagamentoRejeitada : Event
    {
        public string FaturaId { get; }

        public TransacaoPagamentoRejeitada(string faturaId)
        {
            FaturaId = faturaId;
        }
    }
}
