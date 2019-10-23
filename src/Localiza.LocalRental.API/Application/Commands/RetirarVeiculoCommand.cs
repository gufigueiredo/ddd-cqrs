using System;
using Flunt.Validations;
using SC.SDK.NetStandard.DomainCore.Commands;

namespace Localiza.LocalRental.API.Application.Commands
{
    public class RetirarVeiculoCommand : Command
    {
        public string NumeroControleAluguel { get; set; }
        public string NumeroPlaca { get; set; }
        public DateTime? DataHoraRetirada { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(NumeroControleAluguel,
                    "NumeroControleAluguel", "Número de controle do aluguel deve ser informado")
            );

            AddNotifications(new Contract()
                .Requires()
                .HasLen(NumeroPlaca, 7, "Placa", "Placa deve conter 7 caracteres")
            );

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(DataHoraRetirada, "DataHoraRetirada", "Data/Hora retirada deve ser informada")
            );
        }
    }
}
