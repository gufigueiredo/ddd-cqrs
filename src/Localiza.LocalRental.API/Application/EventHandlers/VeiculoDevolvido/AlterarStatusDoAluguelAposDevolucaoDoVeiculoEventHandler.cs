using System.Threading;
using System.Threading.Tasks;
using Localiza.LocalRental.Domain.Model.Aluguel;
using SC.SDK.NetStandard.DomainCore.Handlers;

namespace Localiza.LocalRental.API.Application.EventHandlers.VeiculoDevolvido
{
    public class AlterarStatusDoAluguelAposDevolucaoDoVeiculoEventHandler : DomainEventHandler<Domain.Events.VeiculoDevolvido>
    {
        private readonly IAluguelRepository _aluguelRepository;

        public AlterarStatusDoAluguelAposDevolucaoDoVeiculoEventHandler(IAluguelRepository aluguelRepository)
        {
            _aluguelRepository = aluguelRepository;
        }

        public override Task Handle(Domain.Events.VeiculoDevolvido notification, CancellationToken cancellationToken)
        {
            var aluguel = _aluguelRepository.ObterPorId(notification.AluguelId);
            aluguel.SinalizarVeiculoDevolvido(notification.DataHoraDevolucao);
            _aluguelRepository.Update(aluguel);

            return Task.CompletedTask;
        }
    }
}
