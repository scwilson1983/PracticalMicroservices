using Microsoft.Extensions.DependencyInjection;
using PracticalMicroservices.MaterializedViews.Videos;

namespace PracticalMicroservices.MaterializedViews.Infrastructure.Dependencies
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMaterializedViews(this IServiceCollection services)
        {
            services.AddTransient<IVideoService, VideoService>();
            services.AddDbContext<MaterializedViewsContext>();
        }
    }
}
