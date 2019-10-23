using Flunt.Validations;
using SC.SDK.NetStandard.DomainCore.Commands;

namespace Localiza.LocalRental.API.Application.Commands
{
  public class FecharAluguelCommand : Command
  {
    public string AluguelId { get; set; }

    public override void Validate()
    {
      AddNotifications(new Contract()
        .Requires()
        .IsNotNullOrWhiteSpace(AluguelId, "AluguelId", "Id do aluguel deve ser informado")        
      );
    }
  }
}