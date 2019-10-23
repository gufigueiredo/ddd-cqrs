using Flunt.Validations;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Cliente
{
    public class Telefone : ValueObject
    {
        public Telefone(int ddd, int numero)
        {
            AddNotifications(new Contract()
              .Requires()
              .IsGreaterOrEqualsThan(ddd, 11, "DDD", "DDD deve estar entre 11 e 98")
              .IsLowerThan(ddd, 99, "DDD", "DDD deve estar entre 11 e 98")
            );

            AddNotifications(new Contract()
              .Requires()
              .IsGreaterThan(numero, 0, "Numero", "Número de telefone deve ser positivo")
              .HasMaxLen(numero.ToString(), 9, "Numero", "Número de telefone de conter 8 ou 9 posições numéricas")
              .HasMinLen(numero.ToString(), 8, "Numero", "Número de telefone de conter 8 ou 9 posições numéricas")
            );

            if (Valid)
            {
                Ddd = ddd;
                Numero = numero;
            }
        }

        public string TelefoneComDdd { get => $"{Ddd}{Numero}"; }
        public int Ddd { get; }
        public int Numero { get; }
    }
}
