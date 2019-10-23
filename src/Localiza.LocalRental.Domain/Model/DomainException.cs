using System;
namespace Localiza.LocalRental.Domain.Model
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
        }
    }
}
