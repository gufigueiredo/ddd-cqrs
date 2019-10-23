using System;
using Flunt.Validations;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Veiculo
{
    public class GrupoVeiculo : ValueObject
    {
        public string Nome { get; private set; }
        public decimal MultiplicadorHoraBase { get; private set; }

        protected GrupoVeiculo(string nome, decimal multiplicadorHoraBase)
        {
            AddNotifications(new Contract()
              .Requires()
              .IsNotNullOrWhiteSpace(nome, "Nome", "Nome do grupo deve ser informado")
            );

            AddNotifications(new Contract()
              .Requires()
              .IsGreaterThan(multiplicadorHoraBase, 0, "MultiplicadorHoraBase",
                "Multiplicador deve ser maior que zero")
            );

            if (Valid)
            {
                Nome = nome;
                MultiplicadorHoraBase = multiplicadorHoraBase;
            }
        }
    }
}
