using System;
using System.Collections.Generic;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Cliente
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente ObterPorId(string id);
        IEnumerable<Cliente> ListarTodos();
    }
}
