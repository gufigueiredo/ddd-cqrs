using System;
using SC.SDK.NetStandard.DomainCore.Events;

namespace Localiza.LocalRental.Domain.Events
{
    public class FaturaEmitida : Event
    {
        public string FaturaId { get; }

        public FaturaEmitida(string faturaId)
        {
            FaturaId = faturaId;
        }
    }
}
