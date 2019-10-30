using System;
using Flunt.Validations;
using Newtonsoft.Json;
using SC.SDK.NetStandard.DomainCore.Commands;

namespace Localiza.LocalRental.API.Application.Commands
{
    public class PagarFaturaCommand : Command
    {
        [JsonIgnore]
        internal string FaturaId { get; set; }

        public string NumeroCartaoCredito { get; set; }
        public string CodigoSegurancaCartao { get; set; }
        public int MesValidadeCartao { get; set; }
        public int AnoValidadeCartao { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(NumeroCartaoCredito,
                    "NumeroCartaoCredito", "Número do cartão de crédito deve ser informado")
            );

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(CodigoSegurancaCartao,
                    "CodigoSegurancaCartao", "Código de segurança do deve ser informado")
            );

            AddNotifications(new Contract()
                .Requires()
                .IsBetween(MesValidadeCartao, 1, 12,
                    "MesValidadeCartao", "Mês de validade inválido")
            );

            AddNotifications(new Contract()
                .Requires()
                .IsGreaterOrEqualsThan(AnoValidadeCartao, DateTime.Now.Year,
                    "AnoValidadeCartao", "Ano de validade deve ser maior ou igual ao ano atual")
            );
        }
    }
}
