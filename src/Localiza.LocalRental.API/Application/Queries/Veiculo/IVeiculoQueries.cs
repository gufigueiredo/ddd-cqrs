using System;
using System.Collections.Generic;

namespace Localiza.LocalRental.API.Application.Queries.Veiculo
{
    public interface IVeiculoQueries
    {
        IEnumerable<VeiculoResourceModel> ListarTodosVeiculos();
    }
}
