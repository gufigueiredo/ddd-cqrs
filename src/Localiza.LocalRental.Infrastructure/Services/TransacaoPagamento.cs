using System;
namespace Localiza.LocalRental.Infrastructure.Services
{
    public class TransacaoPagamento
    {
        public DateTime DataHoraTransacao { get; set; }
        public string HashConfirmacaoPagamento { get; set; }
        public bool Efetivada { get; set; }
    }
}
