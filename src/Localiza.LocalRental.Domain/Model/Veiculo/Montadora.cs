using System;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Domain.Model.Veiculo
{
    public class Montadora : Enumeration
    {
        public static Montadora Fiat = new Montadora(1, "Fiat");
        public static Montadora Vw = new Montadora(2, "Vw");
        public static Montadora Chevrolet = new Montadora(3, "Chevrolet");
        public static Montadora Ford = new Montadora(4, "Ford");
        public static Montadora Hyundai = new Montadora(5, "Hyundai");

        protected Montadora(int id, string name)
            : base(id, name)
        {
        }
    }
}
