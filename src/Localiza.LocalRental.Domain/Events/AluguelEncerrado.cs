using System;
using SC.SDK.NetStandard.DomainCore.Events;

namespace Localiza.LocalRental.Domain.Events
{
    public class AluguelEncerrado : Event
    {
        public string AluguelId { get; }

        public AluguelEncerrado(string aluguelId)
        {
            AluguelId = aluguelId;
        }
    }
}
