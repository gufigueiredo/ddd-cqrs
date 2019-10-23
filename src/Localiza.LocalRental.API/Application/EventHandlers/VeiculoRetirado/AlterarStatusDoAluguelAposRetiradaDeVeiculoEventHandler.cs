using System;
using System.Threading;
using System.Threading.Tasks;
using Localiza.LocalRental.Domain.Model.Aluguel;
using SC.SDK.NetStandard.DomainCore.Handlers;

namespace Localiza.LocalRental.API.Application.EventHandlers.VeiculoRetirado
{
    public class AlterarStatusDoAluguelAposRetiradaDeVeiculoEventHandler : DomainEventHandler<Domain.Events.VeiculoRetirado>
    {
        private readonly IAluguelRepository _aluguelRepository;

        public AlterarStatusDoAluguelAposRetiradaDeVeiculoEventHandler(IAluguelRepository aluguelRepository)
        {
            _aluguelRepository = aluguelRepository;
        }

        public override Task Handle(Domain.Events.VeiculoRetirado notification, CancellationToken cancellationToken)
        {
            var aluguel = _aluguelRepository.ObterPorId(notification.AluguelId);
            aluguel.SinalizarVeiculoRetirado(notification.DataHoraRetirada, notification.VeiculoId);
            _aluguelRepository.Update(aluguel);

            return Task.CompletedTask;
        }
    }
}
