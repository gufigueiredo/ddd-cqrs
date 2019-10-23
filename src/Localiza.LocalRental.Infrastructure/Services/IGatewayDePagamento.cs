using System;
using System.Threading.Tasks;

namespace Localiza.LocalRental.Infrastructure.Services
{
    public interface IGatewayDePagamento
    {
        Task<TransacaoPagamento> EfetuarPagamento(string numeroCartao, string codigoSegurancaCartao,
            int mesValidadeCartao, int anoValidadeCartao, decimal valor);
    }
}
