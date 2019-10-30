using System.Threading;
using System.Threading.Tasks;
using Localiza.LocalRental.API.Application.Commands;
using Localiza.LocalRental.Domain.Model.Veiculo;
using MediatR;
using SC.SDK.NetStandard.DomainCore.Commands;
using SC.SDK.NetStandard.DomainCore.Handlers;

namespace Localiza.LocalRental.API.Application.CommandHandlers
{
    public class DevolverVeiculoHandler : CommandHandler<DevolverVeiculoCommand>
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IMediator _mediator;

        public DevolverVeiculoHandler(IVeiculoRepository veiculoRepository, IMediator mediator)
        {
            _veiculoRepository = veiculoRepository;
            _mediator = mediator;
        }

        public override async Task<CommandResult> Handle(DevolverVeiculoCommand request, CancellationToken cancellationToken)
        {
            var veiculo = _veiculoRepository.ObterPorPlaca(request.NumeroPlaca);
            if (veiculo == null)
                return CommandResult.Error($"O veículo placa {request.NumeroPlaca} não foi encontrado");

            veiculo.Devolver(request.DataHoraDevolucao);
            _veiculoRepository.Update(veiculo);
            await veiculo.RaiseEvents(_mediator);

            return CommandResult.Ok();
        }
    }
}
