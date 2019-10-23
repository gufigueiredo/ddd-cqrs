using System;
using System.Threading;
using System.Threading.Tasks;
using Localiza.LocalRental.Domain.Model.Aluguel;
using Localiza.LocalRental.Domain.Model.Fatura;
using Localiza.LocalRental.Domain.Model.Veiculo;
using Localiza.LocalRental.Domain.Services;
using SC.SDK.NetStandard.DomainCore.Handlers;

namespace Localiza.LocalRental.API.Application.EventHandlers.AluguelEncerrado
{
    public class FaturarAluguelAposEncerramentoEventHandler : DomainEventHandler<Domain.Events.AluguelEncerrado>
    {
        private readonly IAluguelRepository _aluguelRepository;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IFaturaRepository _faturaRepository;
        private readonly IFaturarAluguelService _faturarAluguelService;
        private readonly IParametrosCobranca _parametrosCobranca;
        private readonly ICalculadoraImpostos _calculadoraImpostos;

        public FaturarAluguelAposEncerramentoEventHandler(IAluguelRepository aluguelRepository,
            IVeiculoRepository veiculoRepository, IFaturaRepository faturaRepository,
            IFaturarAluguelService faturarAluguelService, IParametrosCobranca parametrosCobranca,
            ICalculadoraImpostos calculadoraImpostos)
        {
            _aluguelRepository = aluguelRepository;
            _veiculoRepository = veiculoRepository;
            _faturaRepository = faturaRepository;
            _faturarAluguelService = faturarAluguelService;
            _parametrosCobranca = parametrosCobranca;
            _calculadoraImpostos = calculadoraImpostos;
        }

        public override Task Handle(Domain.Events.AluguelEncerrado notification, CancellationToken cancellationToken)
        {
            var aluguel = _aluguelRepository.ObterPorId(notification.AluguelId);
            var veiculo = _veiculoRepository.ObterPorId(aluguel.VeiculoId);

            var fatura = _faturarAluguelService.Processar(aluguel, veiculo, _parametrosCobranca.ValorHoraBaseAluguel,
                _parametrosCobranca.ValorHoraBaseOpcionais, _calculadoraImpostos);

            _faturaRepository.Add(fatura);

            return Task.CompletedTask;
        }
    }
}
