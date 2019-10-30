using System;
using Flunt.Validations;
using SC.SDK.NetStandard.DomainCore.Commands;

namespace Localiza.LocalRental.API.Application.Commands
{
    public class AlugarVeiculoCommand : Command
    {
        public string ClienteId { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
              .Requires()
              .IsNotNullOrEmpty(ClienteId, "ClienteId", "Id do Cliente deve ser informado")
            );
            if (!Guid.TryParse(ClienteId, out _))
                AddNotification("ClienteId", "ID do cliente inválido");
        }
    }
}
