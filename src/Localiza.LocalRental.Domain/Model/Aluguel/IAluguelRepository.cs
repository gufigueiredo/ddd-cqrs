using System;
using System.Collections.Generic;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Aluguel
{
    public interface IAluguelRepository : IRepository<Aluguel>
    {
        IEnumerable<Aluguel> ListarTodos();
        IEnumerable<Aluguel> ListarPorCliente(string clienteId);
        Aluguel ObterPorNumeroControle(string numeroControle);
        Aluguel ObterPorId(string id);
    }
}
