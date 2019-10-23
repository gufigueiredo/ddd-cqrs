using System;
using Flunt.Validations;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Cliente
{
    public class CPF : ValueObject
    {
        public CPF(string numero)
        {
            AddNotifications(new Contract()
              .Requires()
              .IsNotNull(numero, "Numero", "CPF deve ser informado")
              .HasLen(numero, 11, "Numero", "CPF deve conter 11 posições")
            );

            if (Valid)
            {
                Numero = numero;
            }
        }

        public string Numero { get; }
    }
}
