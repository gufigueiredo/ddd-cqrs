using System;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Aluguel
{
    public class SituacaoAluguel : Enumeration
    {
        public static SituacaoAluguel Aberto = new SituacaoAluguel(1, "Aberta");
        public static SituacaoAluguel VeiculoRetirado = new SituacaoAluguel(2, "VeiculoRetirado");
        public static SituacaoAluguel VeiculoDevolvido = new SituacaoAluguel(3, "VeiculoDevolvido");
        public static SituacaoAluguel Encerrado = new SituacaoAluguel(4, "Encerrado");
        public static SituacaoAluguel Faturada = new SituacaoAluguel(5, "Faturado");

        protected SituacaoAluguel(int id, string name)
            : base(id, name)
        {
        }
    }
}
