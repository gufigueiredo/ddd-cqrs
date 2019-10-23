using System;
using System.Collections.Generic;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Veiculo
{
    public interface IVeiculoRepository : IRepository<Veiculo>
    {
        IEnumerable<Veiculo> ListarTodos();
        Veiculo ObterPorPlaca(string numeroPlaca);
        Veiculo ObterPorId(string id);
    }
}
