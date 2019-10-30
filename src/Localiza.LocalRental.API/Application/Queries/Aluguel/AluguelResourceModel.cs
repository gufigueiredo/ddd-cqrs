using System;
using System.Collections.Generic;

namespace Localiza.LocalRental.API.Application.Queries.Aluguel
{
    public class AluguelModel : QueryModelBase
    {
        public string ClienteId { get; set; }
        public string VeiculoId { get; set; }
        public string Veiculo { get; set; }
        public string Placa { get; set; }
        public string NumeroControle { get; set; }
        public DateTime EfetuadaEm { get; set; }
        public DateTime? DataHoraRetiradaVeiculo { get; set; }
        public DateTime? DataHoraDevolucaoVeiculo { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string Situacao { get; set; }
        public List<string> Opcionais { get; set; }
    }
}
