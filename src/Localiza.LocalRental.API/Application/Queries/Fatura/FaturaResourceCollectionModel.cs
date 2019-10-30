using System;
namespace Localiza.LocalRental.API.Application.Queries.Fatura
{
    public class FaturaResourceCollectionModel : QueryModelBase
    {
        public string NumeroControleAluguel { get; set; }
        public string ClienteId { get; set; }
        public decimal? ValorTotal { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}
