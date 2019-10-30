using System;
using System.Collections.Generic;
using Localiza.LocalRental.Domain.Services;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Fatura
{
    public class Cobranca : Entity
    {
        protected Cobranca() { }
        public Cobranca(TipoCobranca tipo, int qtdeHorasUtilizadas)
        {
            if (tipo == TipoCobranca.Aluguel)
                Descricao = "Locação de Veículo";
            else
                Descricao = "Opcional";

            QtdeHorasUtilizadas = qtdeHorasUtilizadas;
        }

        public string Descricao { get; private set; }
        public int QtdeHorasUtilizadas { get; private set; }
        public decimal ValorAFaturar { get; private set; }

        public void Processar(decimal valorHoraBase, decimal? multiplicador)
        {
            if (multiplicador.HasValue)
                ValorAFaturar = valorHoraBase * multiplicador.Value * QtdeHorasUtilizadas;
            else
                ValorAFaturar = valorHoraBase * QtdeHorasUtilizadas;
        }
    }

    public enum TipoCobranca
    {
        Aluguel,
        OpcionalAluguel
    }
}
