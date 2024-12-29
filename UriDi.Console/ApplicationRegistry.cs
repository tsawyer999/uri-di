using System.Collections.Generic;
using StructureMap;
using UriDi.Domain.Services;
using UriDi.Infrastructure.HttpClients;
using UriDi.Models.Configuration;

namespace UriDi.Console
{
    public class ApplicationRegistry
    {
        public static IContainer GetContainer(Dictionary<string, RegionConfiguration> configurations)
        {
            var container = new Container();
            foreach (var entry in configurations)
            {
                container.Configure(x => AddRegistry(x, entry.Key, entry.Value));
            }

            return container;
        }

        private static void AddRegistry(ConfigurationExpression configure, string region, RegionConfiguration configuration)
        {
            configure
                .For<ICustomersHttpClient>()
                .Use<CustomersesHttpClient>()
                .Ctor<RegionConfiguration>("configuration")
                .Is(configuration)
                .Named(region);

            configure
                .For<IInvoicesHttpClient>()
                .Use<InvoicesHttpClient>()
                .Ctor<RegionConfiguration>("configuration")
                .Is(configuration)
                .Named(region);

            configure
                .For<ICustomersService>()
                .Use<CustomersService>()
                .Ctor<ICustomersHttpClient>()
                .Is(c => c.GetInstance<ICustomersHttpClient>(region))
                .Named(region);

            configure
                .For<IInvoicesService>()
                .Use<InvoicesService>()
                .Ctor<IInvoicesHttpClient>()
                .Is(c => c.GetInstance<IInvoicesHttpClient>(region))
                .Named(region);
        }
    }
}
