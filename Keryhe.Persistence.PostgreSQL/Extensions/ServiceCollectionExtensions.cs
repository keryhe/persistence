using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Keryhe.Persistence.PostgreSQL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgreSQLProvider(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IPersistenceProvider, PostgreSQLProvider>();
            services.Configure<PostgreSQLProviderOptions>(config);

            return services;
        }
    }
}
