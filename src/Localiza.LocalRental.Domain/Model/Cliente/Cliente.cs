using System.ComponentModel.DataAnnotations;
using Flunt.Validations;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Cliente
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente(string nome, CPF cpf, string email)
        {
            AddNotifications(new Contract()
              .Requires()
              .IsNotNullOrWhiteSpace(nome, "Nome", "Nome deve ser informado")
            );

            AddNotifications(new Contract()
              .Requires()
              .IsNotNull(cpf, "Cpf", "CPF deve ser informado")
              .Join(cpf)
            );

            AlterarEmail(email);

            if (Valid)
            {
                Nome = nome;
                Cpf = cpf;
            }
            
        }

        public string Nome { get; private set; }
        public CPF Cpf { get; private set; }
        public Telefone Telefone { get; private set; }
        public string Email { get; private set; }

        public void InformarOuAlterarTelefone(Telefone telefone)
        {
            if (telefone != null)
            {
                AddNotifications(telefone.Notifications);
                if (Valid)
                    Telefone = telefone;
            }
        }

        public void AlterarEmail(string email)
        {
            AddNotifications(new Contract()
              .Requires()
              .IsNotNullOrWhiteSpace(email, "Email", "Email deve ser informado")
            );

            bool isEmailValid = email != null && new EmailAddressAttribute().IsValid(email);
            if (!isEmailValid)
            {
                AddNotification("Email", "O email informado não é válido");
            }

            if (Valid)
                Email = email;
        }
    }
}
