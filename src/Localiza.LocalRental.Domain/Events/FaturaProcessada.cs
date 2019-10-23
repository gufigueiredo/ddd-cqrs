using System;
using SC.SDK.NetStandard.DomainCore.Events;

namespace Localiza.LocalRental.Domain.Events
{
    public class FaturaProcessada : Event
    {
        public readonly string ReservaId;
        public readonly string FaturaId;

        public FaturaProcessada(string reservaId, string faturaId)
        {
            ReservaId = reservaId;
            FaturaId = faturaId;
        }
    }
}
