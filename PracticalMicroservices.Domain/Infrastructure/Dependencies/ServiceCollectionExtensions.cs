using Microsoft.Extensions.DependencyInjection;
using PracticalMicroservices.Domain.Home;
using PracticalMicroservices.MaterializedViews.Infrastructure.Dependencies;

namespace PracticalMicroservices.Domain.Infrastructure.Dependencies
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddMaterializedViews();
            services.AddTransient<IHomeAggregator, HomeAggregator>();
        }
    }
}
