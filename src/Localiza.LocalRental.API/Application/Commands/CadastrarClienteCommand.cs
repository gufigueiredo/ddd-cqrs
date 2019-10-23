using System;
using Flunt.Validations;
using SC.SDK.NetStandard.DomainCore.Commands;

namespace Localiza.LocalRental.API.Application.Commands
{
    public class CadastrarClienteCommand : Command
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int? TelefoneDdd { get; set; }
        public int? TelefoneNumero { get; set; }
        public string Email { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome,
                    "Nome", "Nome do cliente deve ser informado")
            );

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Email,
                    "Email", "Email do cliente deve ser informado")
            );

            AddNotifications(new Contract()
                .Requires()
                .HasLen(Cpf, 11, "CPF", "CPF deve ser informado com 11 caracteres")
            );
        }
    }
}
