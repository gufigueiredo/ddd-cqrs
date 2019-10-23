using System;
using System.Threading;
using System.Threading.Tasks;
using Localiza.LocalRental.API.Application.Commands;
using Localiza.LocalRental.Domain.Model.Aluguel;
using Localiza.LocalRental.Domain.Model.Veiculo;
using MediatR;
using SC.SDK.NetStandard.DomainCore.Commands;
using SC.SDK.NetStandard.DomainCore.Handlers;

namespace Localiza.LocalRental.API.Application.CommandHandlers
{
    public class RetirarVeiculoHandler : CommandHandler<RetirarVeiculoCommand>
    {
        private readonly IMediator _mediator;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IAluguelRepository _aluguelRepository;

        public RetirarVeiculoHandler(IMediator mediator, IVeiculoRepository veiculoRepository,
            IAluguelRepository aluguelRepository)
        {
            _mediator = mediator;
            _veiculoRepository = veiculoRepository;
            _aluguelRepository = aluguelRepository;
        }

        public override async Task<CommandResult> Handle(RetirarVeiculoCommand request, CancellationToken cancellationToken)
        {
            var aluguel = _aluguelRepository.ObterPorNumeroControle(request.NumeroControleAluguel);
            if (aluguel == null)
                return CommandResult.Error($"O aluguel com número de controle {request.NumeroControleAluguel} não foi encontrado");

            var veiculo = _veiculoRepository.ObterPorPlaca(request.NumeroPlaca);
            if (veiculo == null)
                return CommandResult.Error($"O veículo placa {request.NumeroPlaca} não foi encontrado");

            veiculo.Retirar(aluguel.Id.ToString(), request.DataHoraRetirada.Value);
            _veiculoRepository.Update(veiculo);
            await veiculo.RaiseEvents(_mediator);

            return CommandResult.Ok();
        }
    }
}
