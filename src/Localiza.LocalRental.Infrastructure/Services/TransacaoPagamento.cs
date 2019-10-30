using System;
namespace Localiza.LocalRental.Infrastructure.Events
{
    public class TransacaoPagamento
    {
        public DateTime DataHoraTransacao { get; set; }
        public string HashConfirmacaoPagamento { get; set; }
        public bool Efetivada { get; set; }
    }
}
