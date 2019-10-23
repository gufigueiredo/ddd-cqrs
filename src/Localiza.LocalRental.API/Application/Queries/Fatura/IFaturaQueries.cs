using System;
using System.Collections.Generic;

namespace Localiza.LocalRental.API.Application.Queries.Fatura
{
    public interface IFaturaQueries
    {
        IEnumerable<FaturaResourceCollectionModel> ListarFaturasPorCliente(string clienteId);
        FaturaResourceModel ObterFaturaPorId(string faturaId);
    }
}
