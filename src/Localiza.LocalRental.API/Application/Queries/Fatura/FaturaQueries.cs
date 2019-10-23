using System;
using System.Collections.Generic;
using Localiza.LocalRental.Domain.Model.Cliente;
using Localiza.LocalRental.Domain.Model.Fatura;

namespace Localiza.LocalRental.API.Application.Queries.Fatura
{
    public class FaturaQueries : IFaturaQueries
    {
        private readonly IFaturaRepository _faturaRepository;
        private readonly IClienteRepository _clienteRepository;

        public FaturaQueries(IFaturaRepository faturaRepository, IClienteRepository clienteRepository)
        {
            _faturaRepository = faturaRepository;
            _clienteRepository = clienteRepository;
        }

        public IEnumerable<FaturaResourceCollectionModel> ListarFaturasPorCliente(string clienteId)
        {
            var model = new List<FaturaResourceCollectionModel>();
            var result = _faturaRepository.ListarPorCliente(clienteId);
            foreach (var entidade in result)
            {
                var cliente = _clienteRepository.ObterPorId(clienteId);

                model.Add(new FaturaResourceCollectionModel
                {
                    Id = entidade.Id.ToString(),
                    NumeroControleAluguel = entidade.NumeroControleAluguel,
                    ClienteId = entidade.ClienteId,
                    NomeCliente = cliente.Nome,
                    ValorTotal = entidade.ValorTotal,
                    DataPagamento = entidade.Pagamento.DataPagamentoFatura
                });
            }

            return model;
        }

        public FaturaResourceModel ObterFaturaPorId(string faturaId)
        {
            var entidade = _faturaRepository.ObterPorId(faturaId);
            if (entidade == null)
                return null;

            var cliente = _clienteRepository.ObterPorId(entidade.ClienteId);

            var model = new FaturaResourceModel
            {
                Id = entidade.Id.ToString(),
                NumeroControleAluguel = entidade.NumeroControleAluguel,
                ClienteId = entidade.ClienteId,
                NomeCliente = cliente.Nome,
                ValorTotal = entidade.ValorTotal,
                ValorBruto = entidade.ValorBruto,
                ValorImpostos = entidade.ValorImpostos,
                DataPagamento = entidade.Pagamento.DataPagamentoFatura
            };
            foreach (var item in entidade.Cobrancas)
            {
                model.Cobrancas.Add(new FaturaCobrancaResourceModel
                {
                    Descricao = item.Descricao,
                    QtdeHorasUtilizadas = item.QtdeHorasUtilizadas,
                    ValorAFaturar = item.ValorAFaturar
                });
            }

            return model;
        }
    }
}
