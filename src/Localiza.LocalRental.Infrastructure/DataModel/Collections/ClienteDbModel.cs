using System;
namespace Localiza.LocalRental.Infrastructure.DataModel.Collections
{
    public class ClienteDbModel : DbModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public int TelefoneDdd { get; set; }
        public int TelefoneNumero { get; set; }
    }
}
