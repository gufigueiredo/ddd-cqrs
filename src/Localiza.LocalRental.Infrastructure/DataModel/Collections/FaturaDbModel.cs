using System;
using System.Collections.Generic;

namespace Localiza.LocalRental.Infrastructure.DataModel.Collections
{
    public class FaturaDbModel : DbModel
    {
        public List<CobrancaDbModel> Cobrancas { get; set; }
        public PagamentoFaturaDbModel Pagamento { get; set; }
        public string NumeroControleAluguel { get; set; }
        public string ClienteId { get; set; }
        public decimal? ValorBruto { get; set; }
        public decimal? ValorImpostos { get; set; }
    }

    public class CobrancaDbModel
    {
        public string Descricao { get; set; }
        public int QtdeHorasUtilizadas { get; set; }
        public decimal ValorAFaturar { get; set; }
    }

    public class PagamentoFaturaDbModel
    {
        public DateTime DataPagamentoFatura { get; set; }
        public string HashAutenticacao { get; set; }
    }
}
