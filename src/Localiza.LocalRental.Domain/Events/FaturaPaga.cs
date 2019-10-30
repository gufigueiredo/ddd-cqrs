using System;
using SC.SDK.NetStandard.DomainCore.Events;

namespace Localiza.LocalRental.Domain.Events
{
    public class FaturaPaga : Event
    {
        public string FaturaId { get; }

        public FaturaPaga(string faturaId)
        {
            FaturaId = faturaId;
        }
    }
}
