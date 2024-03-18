using Microsoft.EntityFrameworkCore;

using ProviderTwo.ApplicationServices.Implementation;
using ProviderTwo.ApplicationServices.Interfaces;
using ProviderTwo.DataAccess.InMemory;
using ProviderTwo.DataAccess.InMemory.Repositories;
using ProviderTwo.DataAccess.Interfaces;
using ProviderTwo.UseCases.Services;

using SystemAggregator.Clients.ProviderTwo;
using SystemAggregator.Services;

namespace ProviderTwo.Site.Extensions
{
    public static class HostBuilderExtensions
    {
        public static void AddDataAccessInMemory(this IHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("ProviderTwoDb");

                services.AddDbContext<ProviderTwoDbContext>(
                    options => options.UseInMemoryDatabase(connectionString));

                services.AddTransient<IRouteRepository, RouteRepository>();
            });
        }

        public static void AddDomainServices(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
            });
        }

        public static void AddApplicationServices(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient<IHealthCheckService, HealthCheckService>();
            });
        }

        public static void AddUseCases(this IHostBuilder builder)
        {
            builder.AddProviderTwoClient();

            builder.ConfigureServices((context, services) =>
            {
                services.AddTransient(GetSearchService);
                services.AddTransient<ISearchService, RouteSearchService>();
            });
        }

        private static Func<string, ISearchService?> GetSearchService(IServiceProvider services)
        {
            return searchCode => services.GetServices<ISearchService>().FirstOrDefault(x => x.SearchCode == searchCode);
        }
    }
}
