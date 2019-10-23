using System.Threading;
using System.Threading.Tasks;
using Localiza.LocalRental.API.Application.Commands;
using Localiza.LocalRental.Domain.Model.Aluguel;
using Localiza.LocalRental.Domain.Model.Fatura;
using MediatR;
using SC.SDK.NetStandard.DomainCore.Commands;
using SC.SDK.NetStandard.DomainCore.Handlers;

namespace Localiza.LocalRental.API.Application.CommandHandlers
{
    public class EncerrarAluguelCommandHandler : CommandHandler<FecharAluguelCommand>
    {
        private readonly IAluguelRepository _aluguelRepository;
        private readonly IMediator _mediator;

        public EncerrarAluguelCommandHandler(IAluguelRepository aluguelRepository, IMediator mediator)
        {
            _aluguelRepository = aluguelRepository;
            _mediator = mediator;
        }
        public override async Task<CommandResult> Handle(FecharAluguelCommand request, CancellationToken cancellationToken)
        {
            var aluguel = _aluguelRepository.ObterPorId(request.AluguelId);

            if (aluguel.EstaEncerrado)
                return CommandResult.Error($"O Aluguel {aluguel.NumeroControle} já está fechado.");

            aluguel.Encerrar();
            await aluguel.RaiseEvents(_mediator);

            return CommandResult.Ok();
        }
    }
}