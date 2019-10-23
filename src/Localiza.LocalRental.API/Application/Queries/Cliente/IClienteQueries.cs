using System;
using System.Collections.Generic;

namespace Localiza.LocalRental.API.Application.Queries.Cliente
{
    public interface IClienteQueries
    {
        ClienteResourceModel ObterClientePorId(string id);
        IEnumerable<ClienteResourceModel> ListarTodosClientes();
    }
}
