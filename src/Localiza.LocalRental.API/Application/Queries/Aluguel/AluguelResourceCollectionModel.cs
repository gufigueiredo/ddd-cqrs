using System;
namespace Localiza.LocalRental.API.Application.Queries.Aluguel
{
    public class AluguelResourceCollectionModel : QueryModelBase
    {
        public string ClienteId { get; set; }
        public string VeiculoId { get; set; }
        public string NumeroControle { get; set; }
        public DateTime EfetuadaEm { get; set; }
        public string Situacao { get; set; }
    }
}
