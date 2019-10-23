using System;
using SC.SDK.NetStandard.DomainCore.Commands;

namespace Localiza.LocalRental.API.Application.Commands
{
    public class PagarFaturaCommand : Command
    {
        public string FaturaId { get; set; }
        public string NumeroCartaoCredito { get; set; }
        public string CodigoSegurancaCartao { get; set; }
        public int MesValidadeCartao { get; set; }
        public int AnoValidadeCartao { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
