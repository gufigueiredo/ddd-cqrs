using System;
using System.Collections.Generic;
using Localiza.LocalRental.Domain.Model.Cliente;

namespace Localiza.LocalRental.API.Application.Queries.Cliente
{
    public class ClienteQueries : IClienteQueries
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteQueries(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IEnumerable<ClienteResourceModel> ListarTodosClientes()
        {
            var model = new List<ClienteResourceModel>();
            var result = _clienteRepository.ListarTodos();

            foreach (var entidade in result)
            {
                model.Add(MapResultToResource(entidade));
            }
            return model;
        }

        public ClienteResourceModel ObterClientePorId(string id)
        {
            var entidade = _clienteRepository.ObterPorId(id);

            if (entidade == null)
                return null;

            return MapResultToResource(entidade);
        }

        private ClienteResourceModel MapResultToResource(Domain.Model.Cliente.Cliente entidade)
        {
            return new ClienteResourceModel
            {
                Nome = entidade.Nome,
                Telefone = entidade.Telefone.TelefoneComDdd,
                Cpf = entidade.Cpf.Numero,
                Email = entidade.Email
            };
        }
    }
}
