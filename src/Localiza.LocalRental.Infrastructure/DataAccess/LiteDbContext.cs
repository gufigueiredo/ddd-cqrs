using System;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Localiza.LocalRental.Infrastructure.DataAccess
{
    public class LiteDbContext : ILiteDbContext
    {
        public LiteDatabase Context { get; }

        public LiteDbContext(IOptions<LiteDbConfig> configs)
        {
            try
            {
                var db = new LiteDatabase(configs.Value.DatabasePath);
                if (db != null)
                    Context = db;
            }
            catch (Exception ex)
            {
                throw new Exception("Can find or create LiteDb database.", ex);
            }
        }
    }
}
