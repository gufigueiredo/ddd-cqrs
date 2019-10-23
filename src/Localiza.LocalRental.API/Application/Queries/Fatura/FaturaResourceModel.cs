using System;
using System.Collections.Generic;

namespace Localiza.LocalRental.API.Application.Queries.Fatura
{
    public class FaturaResourceModel : QueryModelBase
    {
        public List<FaturaCobrancaResourceModel> Cobrancas { get; set; }
        public string NumeroControleAluguel { get; set; }
        public string ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? ValorBruto { get; set; }
        public decimal? ValorImpostos { get; set; }
        public DateTime? DataPagamento { get; set; }

        public FaturaResourceModel()
        {
            Cobrancas = new List<FaturaCobrancaResourceModel>();
        }
    }

    public class FaturaCobrancaResourceModel
    {
        public string Descricao { get; set; }
        public int QtdeHorasUtilizadas { get; set; }
        public decimal ValorAFaturar { get; set; }
    }
}
