using System;
namespace Localiza.LocalRental.Domain.Services
{
    public class DomainServiceException : Exception
    {
        public DomainServiceException(string message)
            : base(message)
        {
        }
    }
}
