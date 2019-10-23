using System;
namespace Localiza.LocalRental.Infrastructure.DataModel
{
    public abstract class DbModel
    {
        public string Id { get; set; }
        protected string _id => Id;
    }
}
