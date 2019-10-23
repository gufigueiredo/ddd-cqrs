using Microsoft.Extensions.DependencyInjection;

namespace Localiza.LocalRental.Infrastructure.DataAccess
{
    public static class LiteDbServiceExtension
    {
        public static void AddLiteDb(this IServiceCollection services, string databasePath)
        {
            services.AddTransient<LiteDbContext, LiteDbContext>();
            services.Configure<LiteDbConfig>(options => options.DatabasePath = databasePath);
        }
    }
}
