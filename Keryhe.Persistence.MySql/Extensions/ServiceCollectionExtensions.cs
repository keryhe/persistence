using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Keryhe.Persistence.MySql.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMySqlProvider(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IPersistenceProvider, MySqlProvider>();
            services.Configure<MySqlProviderOptions>(config);

            return services;
        }
    }
}
