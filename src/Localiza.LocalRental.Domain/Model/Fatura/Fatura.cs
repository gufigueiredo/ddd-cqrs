using System;
using System.Collections.Generic;
using System.Linq;
using Localiza.LocalRental.Domain.Events;
using Localiza.LocalRental.Domain.Services;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Fatura
{
    public class Fatura : Entity, IAggregateRoot
    {
        public Fatura(string numeroControleAluguel, string clienteId)
        {
            NumeroControleAluguel = numeroControleAluguel;
            ClienteId = clienteId;
            Cobrancas = new List<Cobranca>();
        }

        public virtual ICollection<Cobranca> Cobrancas { get; private set; }
        public string NumeroControleAluguel { get; private set; }
        public string ClienteId { get; private set; }
        public decimal? ValorTotal
        {
            get => ValorBruto + ValorImpostos;
        }
        public decimal? ValorBruto { get; private set; }
        public decimal? ValorImpostos { get; private set; }
        public PagamentoFatura Pagamento { get; private set; }
        public DateTime? DataInicioTransacaoPagamento { get; private set; }
        public bool EstaPaga { get => Pagamento != null; }
        public bool EstaCalculada { get => ValorTotal.HasValue; }

        public void AdicionarCobranca(Cobranca cobranca)
        {
            if (cobranca == null)
            {
                AddNotification("Cobranca", $"Cobranca deve ser informada");
            }
            else
            {
                Cobrancas.Add(cobranca);
            }
        }

        public void Calcular(ICalculadoraImpostos calculadoraImpostos)
        {
            ValorBruto = Cobrancas.Sum(c => c.ValorAFaturar);
            ValorImpostos = calculadoraImpostos.CalcularValorImpostos(ValorBruto.Value);
        }

        public void Emitir()
        {
            AddDomainEvent(new FaturaEmitida(this.Id.ToString()));
        }

        public void IniciarTransacaoDePagamento(DateTime dataPagamento)
        {
            if (!EstaCalculada)
                AddNotification("Fatura", "Não foi possível efetuar o pagamento da fatura pois a mesma não foi calculada");
            if (EstaPaga)
                AddNotification("Fatura", "Não foi possível efetuar o pagamento da fatura pois a mesma já encontra-se quitada");

            if (Valid)
            {
                DataInicioTransacaoPagamento = dataPagamento;
            }
        }

        public void ConfirmarTransacaoDePagamento(string hashAutenticacaoPagamento)
        {
            if (!DataInicioTransacaoPagamento.HasValue)
                AddNotification("Fatura", "Não foi possível confirmar a transação de pagamento pois a mesma não foi iniciada");

            if (Valid)
            {
                Pagamento = new PagamentoFatura(DataInicioTransacaoPagamento.Value, hashAutenticacaoPagamento);
                AddDomainEvent(new FaturaPaga(this.Id.ToString()));
            }
        }

        public void RejeitarTransacaoDePagamento()
        {
            DataInicioTransacaoPagamento = null;
            AddDomainEvent(new TransacaoPagamentoRejeitada(this.Id.ToString()));
        }
    }
}
