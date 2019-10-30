using System;
using System.Threading.Tasks;

namespace Localiza.LocalRental.Infrastructure.Events
{
    public class GatewayDePagamento : IGatewayDePagamento
    {
        public async Task<TransacaoPagamento> EfetuarPagamento(string numeroCartao, string codigoSegurancaCartao, int mesValidadeCartao, int anoValidadeCartao, decimal valor)
        {
            Random rnd = new Random();
            bool transacaoEfetuada = rnd.Next(2) == 0;
            await Task.Delay(1000);

            if (transacaoEfetuada)
            {
                return new TransacaoPagamento()
                {
                    DataHoraTransacao = DateTime.Now,
                    Efetivada = true,
                    HashConfirmacaoPagamento = Guid.NewGuid().ToString()
                };
            }
            return new TransacaoPagamento() { Efetivada = false };
        }
    }
}
