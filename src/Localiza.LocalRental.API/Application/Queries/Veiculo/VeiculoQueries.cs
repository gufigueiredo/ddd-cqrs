using System;
using System.Collections.Generic;
using Localiza.LocalRental.Domain.Model.Veiculo;

namespace Localiza.LocalRental.API.Application.Queries.Veiculo
{
    public class VeiculoQueries : IVeiculoQueries
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoQueries(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        public IEnumerable<VeiculoResourceModel> ListarTodosVeiculos()
        {
            var model = new List<VeiculoResourceModel>();
            var result = _veiculoRepository.ListarTodos();

            foreach (var entidade in result)
            {
                model.Add(new VeiculoResourceModel()
                {
                    Id = entidade.Id.ToString(),
                    AluguelId = entidade.AluguelId,
                    NumeroPlaca = entidade.NumeroPlaca,
                    MarcaModelo = $"{entidade.ModeloVeiculo.Montadora.Name}/{entidade.ModeloVeiculo.Modelo}",
                    Cor = entidade.Cor.ToString(),
                    Disponivel = entidade.EstaDisponivel
                });
            }

            return model;
        }
    }
}
