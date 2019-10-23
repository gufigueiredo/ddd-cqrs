using System;
namespace Localiza.LocalRental.API.Application.Queries.Cliente
{
    public class ClienteResourceModel : QueryModelBase
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
