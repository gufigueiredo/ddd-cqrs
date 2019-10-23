using System;
using Flunt.Validations;
using SC.SDK.NetStandard.DomainCore.Commands;

namespace Localiza.LocalRental.API.Application.Commands
{
    public class DevolverVeiculoCommand : Command
    {
        public string NumeroPlaca { get; set; }
        public DateTime DataHoraDevolucao { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(NumeroPlaca, "NumeroPlaca", "Número da placa deve ser informado")
                .HasLen(NumeroPlaca, 7, "NumeroPlaca", "Número da placa deve conter 7 posições")
            );
        }
    }
}
