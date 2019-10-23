using System;
using System.Collections.Generic;

namespace Localiza.LocalRental.API.Application.Queries.Aluguel
{
    public interface IAluguelQueries
    {
        AluguelModel ObterAluguel(string id);
        IEnumerable<AluguelResourceCollectionModel> ListarTodosAlugueis();
    }
}
