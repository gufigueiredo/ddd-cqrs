using System;
namespace Localiza.LocalRental.API.Application.Queries.Veiculo
{
    public class VeiculoResourceModel : QueryModelBase
    {
        public string AluguelId { get; set; }
        public string NumeroPlaca { get; set; }
        public string MarcaModelo { get; set; }
        public string Cor { get; set; }
        public bool Disponivel { get; set; }
    }
}
