using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PracticalMicroservices.Events.Infrastructure.Db;
using PracticalMicroservices.Events.Services;

namespace PracticalMicroservices.Events.Infrastructure.Dependencies
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEvents(this IServiceCollection services)
        {
            services.AddTransient<IMessageStore, MessageStore>();
            services.AddDbContext<MessageStoreContext>(options =>
            {
                options.UseNpgsql("Host=localhost;User Id=postgres;Password=Wynyard_2020;Port=5432;Database=message_store;");
            });
        }
    }
}
