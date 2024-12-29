using StructureMap;
using UriDi.Domain.Services;
using UriDi.Infrastructure.HttpClients;
using UriDi.Models.Configuration;

namespace UriDi.Console.Configuration
{
    public class RegionRegistry : Registry
    {
        public RegionRegistry(string region, RegionConfiguration configuration)
        {
            For<ICustomersHttpClient>()
                .Use<CustomersesHttpClient>()
                .Ctor<RegionConfiguration>("configuration")
                .Is(configuration)
                .Named(region);

            For<IInvoicesHttpClient>()
                .Use<InvoicesHttpClient>()
                .Ctor<RegionConfiguration>("configuration")
                .Is(configuration)
                .Named(region);

            For<ICustomersService>()
                .Use<CustomersService>()
                .Ctor<ICustomersHttpClient>()
                .Is(c => c.GetInstance<ICustomersHttpClient>(region))
                .Named(region);

            For<IInvoicesService>()
                .Use<InvoicesService>()
                .Ctor<IInvoicesHttpClient>()
                .Is(c => c.GetInstance<IInvoicesHttpClient>(region))
                .Named(region);
        }
    }
}
