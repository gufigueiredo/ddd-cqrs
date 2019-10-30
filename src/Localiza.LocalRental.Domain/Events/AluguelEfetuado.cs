using System;
using SC.SDK.NetStandard.DomainCore.Events;

namespace Localiza.LocalRental.Domain.Events
{
    public class AluguelEfetuado : Event
    {
        public string NumeroControle { get; }
        public string ClienteId { get; }

        public AluguelEfetuado(string numeroControle, string clienteId) 
        {
            NumeroControle = numeroControle;
            ClienteId = clienteId;
        }
    }
}
