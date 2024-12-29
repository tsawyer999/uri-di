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
            var registry = new Registry();
            foreach (var entry in configurations)
            {
                registry.Profile(entry.Key, profile => { CreateProfile(profile, entry.Value); });
            }

            var container = new Container();
            container.Configure(config => config.AddRegistry(registry));

            return container;
        }

        private static void CreateProfile(IProfileRegistry profile, RegionConfiguration configuration)
        {
            profile
                .For<ICustomersHttpClient>()
                .Use<CustomersesHttpClient>()
                .Ctor<RegionConfiguration>("configuration")
                .Is(configuration);

            profile
                .For<IInvoicesHttpClient>()
                .Use<InvoicesHttpClient>()
                .Ctor<RegionConfiguration>("configuration")
                .Is(configuration);

            profile
                .For<ICustomersService>()
                .Use<CustomersService>();

            profile
                .For<IInvoicesService>()
                .Use<InvoicesService>();
        }


    }
}
