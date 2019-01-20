using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Keryhe.Persistence.SqlServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerProvider(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IPersistenceProvider, SqlServerProvider>();
            services.Configure<SqlServerProviderOptions>(config);

            return services;
        }
    }
}
