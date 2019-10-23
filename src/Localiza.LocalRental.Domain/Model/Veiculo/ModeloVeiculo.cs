using System;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Veiculo
{
    public class ModeloVeiculo : ValueObject
    {
        public ModeloVeiculo(Montadora montadora, string modelo, GrupoVeiculo grupo)
        {
            Montadora = montadora ?? throw new ArgumentNullException(nameof(montadora));
            Modelo = modelo ?? throw new ArgumentNullException(nameof(modelo));
            Grupo = grupo ?? throw new ArgumentNullException(nameof(grupo));
        }

        public Montadora Montadora { get; private set; }
        public string Modelo { get; private set; }
        public GrupoVeiculo Grupo { get; private set; }
    }
}
