using System;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Fatura
{
    public class PagamentoFatura : ValueObject
    {
        public DateTime DataPagamentoFatura { get; private set; }
        public string HashAutenticacao { get; private set; }

        public PagamentoFatura(DateTime dataPagamentoFatura, string hashAutenticacao)
        {
            DataPagamentoFatura = dataPagamentoFatura;
            HashAutenticacao = hashAutenticacao;
        }
    }
}
