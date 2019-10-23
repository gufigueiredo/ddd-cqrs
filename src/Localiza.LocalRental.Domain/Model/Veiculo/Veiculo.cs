using System;
using Flunt.Validations;
using Localiza.LocalRental.Domain.Events;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Veiculo
{
    public class Veiculo : Entity, IAggregateRoot
    {
        public Veiculo(string numeroPlaca, ModeloVeiculo modeloVeiculo, CorDoVeiculo cor)
        {
            AddNotifications(new Contract()
              .Requires()
              .IsNotNullOrWhiteSpace(numeroPlaca, "Numero", "Número da placa deve ser informado")
            );
            AddNotifications(new Contract()
              .Requires()
              .HasLen(numeroPlaca, 7, "Numero", "Número da placa deve conter 7 posições")
            );
            AddNotifications(new Contract()
              .Requires()
              .IsNotNull(modeloVeiculo, "Modelo", "Modelo do veículo deve ser especificado")
              .Join(modeloVeiculo)
            );

            if (Valid)
            {
                NumeroPlaca = numeroPlaca;
                ModeloVeiculo = modeloVeiculo;
                Cor = cor;
            }
        }

        public string AluguelId { get; private set; }
        public string NumeroPlaca { get; private set; }
        public ModeloVeiculo ModeloVeiculo { get; private set; }
        public CorDoVeiculo Cor { get; private set; }
        public DateTime? DataHoraRetirada { get; private set; }
        public DateTime? DataHoraDevolucao { get; private set; }
        public bool EstaDisponivel { get => AluguelId == null; }

        public void Retirar(string aluguelId, DateTime dataHoraRetirada)
        {
            AddNotifications(new Contract()
              .Requires()
              .IsNotNullOrWhiteSpace(aluguelId, "AluguelId", "ID do aluguel deve ser informado")
            );

            if (!EstaDisponivel)
                AddNotification("Veiculo", "Este veículo não pode ser retirado pois está alugado");

            if (Valid)
            {
                AluguelId = aluguelId;
                DataHoraRetirada = dataHoraRetirada;
                AddDomainEvent(new VeiculoRetirado(this.Id.ToString(), aluguelId, dataHoraRetirada));
            }
        }

        public void Devolver(DateTime dataHoraDevolucao)
        {
            if (EstaDisponivel)
                return;

            AddDomainEvent(new VeiculoDevolvido(this.Id.ToString(), this.AluguelId, dataHoraDevolucao));
            DataHoraDevolucao = dataHoraDevolucao;
            AluguelId = null;
        }
    }
}