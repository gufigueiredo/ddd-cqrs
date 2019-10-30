using System;
using System.Threading;
using System.Threading.Tasks;
using Localiza.LocalRental.API.Application.Commands;
using Localiza.LocalRental.Domain.Model.Fatura;
using Localiza.LocalRental.Infrastructure.Events;
using SC.SDK.NetStandard.DomainCore.Commands;
using SC.SDK.NetStandard.DomainCore.Handlers;

namespace Localiza.LocalRental.API.Application.CommandHandlers
{
    public class PagarFaturaCommandHandler : CommandHandler<PagarFaturaCommand>
    {
        private readonly IFaturaRepository _faturaRepository;
        private readonly IGatewayDePagamento _gatewayDePagamento;

        public PagarFaturaCommandHandler(IFaturaRepository faturaRepository, IGatewayDePagamento gatewayDePagamento)
        {
            _faturaRepository = faturaRepository;
            _gatewayDePagamento = gatewayDePagamento;
        }

        public override async Task<CommandResult> Handle(PagarFaturaCommand request, CancellationToken cancellationToken)
        {
            var fatura = _faturaRepository.ObterPorId(request.FaturaId);
            if (fatura == null)
                return CommandResult.NotFound("Fatura", request.FaturaId);

            fatura.IniciarTransacaoDePagamento(DateTime.Now);
            if (fatura.Valid)
            {
                var transacao = await _gatewayDePagamento.EfetuarPagamento(request.NumeroCartaoCredito, request.CodigoSegurancaCartao,
                    request.MesValidadeCartao, request.AnoValidadeCartao, fatura.ValorTotal.Value);

                CommandResult result;
                if (transacao.Efetivada)
                {
                    fatura.ConfirmarTransacaoDePagamento(transacao.HashConfirmacaoPagamento);
                    result = CommandResult.Ok();
                }
                else
                {
                    fatura.RejeitarTransacaoDePagamento();
                    result = CommandResult.Error("A transação foi rejeitada pela operadora");
                }
                _faturaRepository.Update(fatura);
                return result;
            }

            return CommandResult.Ok();

        }
    }
}
