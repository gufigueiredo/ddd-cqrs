using System;
using System.Collections.Generic;

namespace Localiza.LocalRental.Infrastructure.DataModel.Collections
{
    public class AluguelDbModel : DbModel
    {
        public string ClienteId { get; set; }
        public string VeiculoId { get; set; }
        public string NumeroControle { get; set; }
        public DateTime EfetuadaEm { get; set; }
        public DateTime? DataHoraRetiradaVeiculo { get; set; }
        public DateTime? DataHoraDevolucaoVeiculo { get; set; }
        public DateTime? DataFechamento { get; set; }
        public int Situacao { get; set; }
        public List<int> Opcionais { get; set; }
    }
}
