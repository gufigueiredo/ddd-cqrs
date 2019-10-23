using System;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Aluguel
{
    public class Opcional : Enumeration
    {
        public static Opcional SeguroBasico = new Opcional(1, "Seguro Básico");
        public static Opcional Gps = new Opcional(3, "GPS");
        public static Opcional CadeiraBebe = new Opcional(4, "Cadeira de Bebê");

        protected Opcional(int id, string name)
            : base(id, name)
        {
        }
    }
}
