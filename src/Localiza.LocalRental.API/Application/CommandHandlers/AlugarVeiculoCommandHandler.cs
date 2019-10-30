using System.Threading;
using System.Threading.Tasks;
using Localiza.LocalRental.API.Application.Commands;
using Localiza.LocalRental.Domain.Model.Aluguel;
using MediatR;
using SC.SDK.NetStandard.DomainCore.Commands;
using SC.SDK.NetStandard.DomainCore.Handlers;

namespace Localiza.LocalRental.API.Application.CommandHandlers
{
    public class EfetuarAluguelCommandHandler : CommandHandler<AlugarVeiculoCommand>
    {
        readonly IAluguelRepository _aluguelRepsitory;
        readonly IMediator _mediator;

        public EfetuarAluguelCommandHandler(IAluguelRepository aluguelRepsitory, IMediator mediator)
        {
            _aluguelRepsitory = aluguelRepsitory;
            _mediator = mediator;
        }

        public override Task<CommandResult> Handle(AlugarVeiculoCommand request, CancellationToken cancellationToken)
        {
            var aluguel = new Aluguel(request.ClienteId);
            if (aluguel.Valid)
            {
                aluguel.Efetivar();
                _aluguelRepsitory.Add(aluguel);
                aluguel.RaiseEvents(_mediator);

                return Task.FromResult(CommandResult.Ok(aluguel.Id.ToString(), CommandResultResourceAction.Created));
            }

            return Task.FromResult(CommandResult.Error(aluguel.Notifications));
        }
    }
}
