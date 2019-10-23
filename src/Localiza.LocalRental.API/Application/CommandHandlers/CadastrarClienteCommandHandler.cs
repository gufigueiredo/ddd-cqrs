using System;
using System.Threading;
using System.Threading.Tasks;
using Localiza.LocalRental.API.Application.Commands;
using Localiza.LocalRental.Domain.Model.Cliente;
using SC.SDK.NetStandard.DomainCore.Commands;
using SC.SDK.NetStandard.DomainCore.Handlers;

namespace Localiza.LocalRental.API.Application.CommandHandlers
{
    public class CadastrarClienteCommandHandler : CommandHandler<CadastrarClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;

        public CadastrarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public override Task<CommandResult> Handle(CadastrarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente(request.Nome, new CPF(request.Cpf), request.Email);
            if (request.TelefoneDdd.HasValue &&
                request.TelefoneNumero.HasValue)
            {
                cliente.InformarOuAlterarTelefone(new Telefone(request.TelefoneDdd.Value, request.TelefoneNumero.Value));
            }

            if (cliente.Invalid)
            {
                return Task.FromResult(CommandResult.Error(cliente.Notifications));
            }

            _clienteRepository.Add(cliente);
            return Task.FromResult(CommandResult.Ok(cliente.Id.ToString(), CommandResultResourceAction.Created));
        }
    }
}
