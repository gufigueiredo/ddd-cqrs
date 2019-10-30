using System;
using System.Collections.Generic;
using System.Linq;
using Localiza.LocalRental.Domain.Model.Aluguel;
using Localiza.LocalRental.Domain.Model.Veiculo;

namespace Localiza.LocalRental.API.Application.Queries.Aluguel
{
    public class AluguelQueries : IAluguelQueries
    {
        private readonly IAluguelRepository _aluguelRepository;
        private readonly IVeiculoRepository _veiculoRepository;

        public AluguelQueries(IAluguelRepository aluguelRepository, IVeiculoRepository veiculoRepository)
        {
            _aluguelRepository = aluguelRepository;
            _veiculoRepository = veiculoRepository;
        }

        public IEnumerable<AluguelResourceCollectionModel> ListarTodosAlugueis()
        {
            List<AluguelResourceCollectionModel> model = new List<AluguelResourceCollectionModel>();
            var result = _aluguelRepository.ListarTodos();
            foreach (var entidade in result)
            {
                model.Add(new AluguelResourceCollectionModel()
                {
                    Id = entidade.Id.ToString(),
                    ClienteId = entidade.ClienteId,
                    VeiculoId = entidade.VeiculoId,
                    NumeroControle = entidade.NumeroControle,
                    EfetuadaEm = entidade.EfetuadaEm,
                    Situacao = entidade.Situacao.ToString(),
                });
            }

            return model;
        }

        public AluguelModel ObterAluguel(string id)
        {
            AluguelModel model = null;
            var entidade = _aluguelRepository.ObterPorId(id);
            if (entidade != null)
            {
                var veiculo = _veiculoRepository.ObterPorId(entidade.VeiculoId);

                model = new AluguelModel
                {
                    Id = entidade.Id.ToString(),
                    ClienteId = entidade.ClienteId,
                    VeiculoId = entidade.VeiculoId,
                    Veiculo = veiculo != null ? $"{veiculo.ModeloVeiculo.Montadora.Name} / {veiculo.ModeloVeiculo.Modelo}" : null,
                    Placa = veiculo?.NumeroPlaca,
                    NumeroControle = entidade.NumeroControle,
                    EfetuadaEm = entidade.EfetuadaEm,
                    DataFechamento = entidade.DataFechamento,
                    DataHoraDevolucaoVeiculo = entidade.DataHoraDevolucaoVeiculo,
                    DataHoraRetiradaVeiculo = entidade.DataHoraRetiradaVeiculo,
                    Situacao = entidade.Situacao.ToString(),
                    Opcionais = entidade.Opcionais.Select(o => o.Name).ToList()
                };
            }

            return model;
        }


    }
}
