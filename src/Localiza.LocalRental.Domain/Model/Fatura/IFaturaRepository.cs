using System;
using System.Collections.Generic;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Fatura
{
    public interface IFaturaRepository : IRepository<Fatura>
    {
        Fatura ObterPorId(string id);
        IEnumerable<Fatura> ListarPorCliente(string clienteId);
        IEnumerable<Fatura> ListarTodos();
    }
}
