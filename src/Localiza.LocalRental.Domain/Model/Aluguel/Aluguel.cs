using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using Localiza.LocalRental.Domain.Events;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Aluguel
{
    public class Aluguel : Entity, IAggregateRoot
    {
        public Aluguel(string clienteId)
        {
            AddNotifications(new Contract()
              .Requires()
              .IsNotNullOrEmpty(clienteId, "ClienteId", "Id do Cliente deve ser informado")
            );

            if (Valid)
            {
                ClienteId = clienteId;
                EfetuadaEm = DateTime.Now;
                GerarNumeroDeControle();
                Situacao = SituacaoAluguel.Aberto;
                Opcionais = new List<Opcional>();                
            }
        }

        public string ClienteId { get; private set; }
        public string VeiculoId { get; private set; }
        public string NumeroControle { get; private set; }
        public DateTime EfetuadaEm { get; private set; }
        public DateTime? DataHoraRetiradaVeiculo { get; private set; }
        public DateTime? DataHoraDevolucaoVeiculo { get; private set; }
        public DateTime? DataFechamento { get; private set; }
        public SituacaoAluguel Situacao { get; private set; }
        public virtual ICollection<Opcional> Opcionais { get; private set; }
        public bool EstaEncerrado { get => Situacao == SituacaoAluguel.Encerrado; }
        public bool PossuiOpcionais { get => Opcionais.Count > 0; }

        public void IncluirOpcional(Opcional Opcional)
        {
            if (Opcional == null)
            {
                AddNotification("Opcional", "Opcional deve ser especificado");
            }
            else
            {
                if (!Opcionais.Any(a => a == Opcional))
                    Opcionais.Add(Opcional);
            }
        }

        public void RemoverOpcional(Opcional Opcional)
        {
            Opcionais.Remove(Opcional);
        }

        public void SinalizarVeiculoRetirado(DateTime dataHoraRetirada, string veiculoId)
        {
            AddNotifications(new Contract()
              .Requires()
              .IsNotNullOrEmpty(veiculoId, "VeiculoId", "Id do veículo deve ser informado")
            );

            if (Valid)
            {
                VeiculoId = veiculoId;
                Situacao = SituacaoAluguel.VeiculoRetirado;
                DataHoraRetiradaVeiculo = dataHoraRetirada;
            }
        }

        public void SinalizarVeiculoDevolvido(DateTime dataHoraDevolucao)
        {
            DataHoraDevolucaoVeiculo = dataHoraDevolucao;
            Situacao = SituacaoAluguel.VeiculoDevolvido;
        }

        public void Efetivar()
        {
            AddDomainEvent(new AluguelEfetuado(this.NumeroControle, this.ClienteId));
        }

        public void Encerrar()
        {
            if (Situacao.Id < SituacaoAluguel.VeiculoDevolvido.Id)
            {
                AddNotification("Situacao", $"O aluguel {NumeroControle} não pode ser encerrado pois o veículo não foi devolvido");
            }
            DataFechamento = DateTime.Now;
            Situacao = SituacaoAluguel.Encerrado;
            AddDomainEvent(new AluguelEncerrado(this.Id.ToString()));
        }

        public int HorasUtilizadas()
        {
            return (int)(DataHoraDevolucaoVeiculo - DataHoraRetiradaVeiculo).Value.TotalHours;
        }

        private void GerarNumeroDeControle()
        {
            NumeroControle = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
    }
}
