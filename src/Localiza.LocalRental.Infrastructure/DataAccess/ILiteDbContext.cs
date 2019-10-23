using System;
using LiteDB;

namespace Localiza.LocalRental.Infrastructure.DataAccess
{
    public interface ILiteDbContext
    {
        LiteDatabase Context { get; }
    }
}
