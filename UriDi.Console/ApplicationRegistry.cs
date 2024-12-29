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
                container.Configure(configure =>
                {
                    configure
                        .For<ICustomersHttpClient>()
                        .Use<CustomersesHttpClient>()
                        .Ctor<RegionConfiguration>("configuration")
                        .Is(entry.Value)
                        .Named(entry.Key);

                    configure
                        .For<IInvoicesHttpClient>()
                        .Use<InvoicesHttpClient>()
                        .Ctor<RegionConfiguration>("configuration")
                        .Is(entry.Value)
                        .Named(entry.Key);

                    configure
                        .For<ICustomersService>()
                        .Use<CustomersService>()
                        .Ctor<ICustomersHttpClient>()
                        .Is(c => c.GetInstance<ICustomersHttpClient>(entry.Key))
                        .Named(entry.Key);

                    configure
                        .For<IInvoicesService>()
                        .Use<InvoicesService>()
                        .Ctor<IInvoicesHttpClient>()
                        .Is(c => c.GetInstance<IInvoicesHttpClient>(entry.Key))
                        .Named(entry.Key);
                });
            }

            return container;
        }
    }
}
