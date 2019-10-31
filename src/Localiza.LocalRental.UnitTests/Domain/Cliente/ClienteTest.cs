using System;
using Xunit;

namespace Localiza.LocalRental.UnitTests.Domain.Cliente
{
    public class ClienteTest
    {
        [Fact]
        public void Deve_Instanciar_Entidade_Valida()
        {
            var entidade = new LocalRental.Domain.Model.Cliente.Cliente(
                "NOME", new LocalRental.Domain.Model.Cliente.CPF("46062115044"), "email@email.com");

            Assert.True(entidade.Valid);
            Assert.Equal("NOME", entidade.Nome);
            Assert.IsType<LocalRental.Domain.Model.Cliente.CPF>(entidade.Cpf);
            Assert.Equal("email@email.com", entidade.Email);
        }

        [Fact]
        public void Deve_Validar_Parametros_Constructor()
        {
            var entidade = new LocalRental.Domain.Model.Cliente.Cliente(
                null, null, null);

            Assert.False(entidade.Valid);
            Assert.Equal(2, entidade.Notifications.Count);
        }
    }
}
