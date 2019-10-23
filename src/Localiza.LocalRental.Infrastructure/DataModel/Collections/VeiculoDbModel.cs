using System;

namespace Localiza.LocalRental.Infrastructure.DataModel.Collections
{
    public class VeiculoDbModel : DbModel
    {
        public string AluguelId { get; set; }
        public string NumeroPlaca { get; set; }
        public ModeloVeiculoDbModel ModeloVeiculo { get; set; }
        public int Cor { get; set; }
        public DateTime? DataHoraRetirada { get; set; }
        public DateTime? DataHoraDevolucao { get; set; }
    }

    public class ModeloVeiculoDbModel
    {
        public int Montadora { get; set; }
        public string Modelo { get; set; }
        public GrupoVeiculoDbModel Grupo { get; set; }
    }

    public class GrupoVeiculoDbModel
    {
        public string Nome { get; set; }
        public decimal MultiplicadorHoraBase { get; set; }
    }
}
