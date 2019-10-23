using System;
using Localiza.LocalRental.Domain.Model.Fatura;
using Localiza.LocalRental.Domain.Model.Aluguel;
using SC.SDK.NetStandard.DomainCore;
using Localiza.LocalRental.Domain.Model.Veiculo;

namespace Localiza.LocalRental.Domain.Services
{
    /// <summary>
    /// Fatura uma locação e seus opcionais
    /// </summary>
    public class FaturarAluguelService : DomainService, IFaturarAluguelService
    {
        public Fatura Processar(Aluguel aluguel, Veiculo veiculo, decimal valorHoraBaseAluguel,
            decimal valorHoraBaseOpcionais, ICalculadoraImpostos calculadoraImpostos)
        {
            Fatura fatura = new Fatura(aluguel.NumeroControle);

            if (!aluguel.EstaEncerrado)
                throw new DomainServiceException($"O Aluguel {aluguel.NumeroControle} não pôde ser faturado pois ainda não foi encerrado.");

            var horasUtilizadas = aluguel.HorasUtilizadas();
            var multiplicador = veiculo.ModeloVeiculo.Grupo.MultiplicadorHoraBase;

            Cobranca cobranca = new Cobranca(TipoCobranca.Aluguel, horasUtilizadas);
            cobranca.Processar(valorHoraBaseAluguel, multiplicador);
            fatura.AdicionarCobranca(cobranca);

            if (aluguel.PossuiOpcionais)
            {
                foreach (var opcional in aluguel.Opcionais)
                {
                    Cobranca cobrancaOpcional = new Cobranca(TipoCobranca.OpcionalAluguel, horasUtilizadas);
                    cobrancaOpcional.Processar(valorHoraBaseOpcionais, null);
                    fatura.AdicionarCobranca(cobrancaOpcional);
                }
            }

            fatura.Calcular(calculadoraImpostos);

            return fatura;
        }
    }

    public interface IFaturarAluguelService
    {
        Fatura Processar(Aluguel aluguel, Veiculo veiculo, decimal valorHoraBaseAluguel,
            decimal valorHoraBaseOpcionais, ICalculadoraImpostos calculadoraImpostos);
    }
}
