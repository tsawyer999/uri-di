using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StructureMap;
using UriDi.Domain.Services;
using UriDi.Infrastructure.HttpClients;
using UriDi.Models.Configuration;

namespace UriDi.Console
{
    public class Program
    {
        public static async Task Main()
        {
            var configurations = GetConfiguration();
            var container = GetContainer(configurations);

            await QueryCustomers(container.GetProfile(Region.US));
            await QueryInvoices(container.GetProfile(Region.EU));
            
            System.Console.WriteLine("\n\nPROCESS DONE");
        }

        private static IContainer GetContainer(Dictionary<string, RegionConfiguration> configurations)
        {
            var registry = new Registry();
            foreach (var entry in configurations)
            {
                registry.Profile(entry.Key, profile =>
                {
                    CreateProfile(profile, entry.Value);
                });
            }

            var container = new Container();
            container.Configure(config => config.AddRegistry(registry));
            
            return container;
        }

        private static void CreateProfile(IProfileRegistry profile, RegionConfiguration configuration)
        {
            profile
                .For<ICustomerHttpClient>()
                .Use<CustomerHttpClient>()
                .Ctor<RegionConfiguration>("configuration")
                .Is(configuration);

            profile
                .For<IInvoiceHttpClient>()
                .Use<InvoiceHttpClient>()
                .Ctor<RegionConfiguration>("configuration")
                .Is(configuration);

            profile
                .For<ICustomersService>()
                .Use<CustomersService>();

            profile
                .For<IInvoicesService>()
                .Use<InvoicesService>();
        }

        private static Dictionary<string, RegionConfiguration> GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();
            
            var configurations = new Dictionary<string, RegionConfiguration>();

            foreach (var region in Region.All)
            {
                var regionConfiguration = new RegionConfiguration();
                configuration.GetSection(region).Bind(regionConfiguration);
                configurations.Add(region, regionConfiguration);
            }

            return configurations;
        }

        private static async Task QueryCustomers(IContainer container)
        {
            var customersService = container.GetInstance<ICustomersService>();
            var customers = await customersService.GetOddCustomersAsync();

            foreach (var customer in customers)
            {
                System.Console.WriteLine(customer);
            }
        }

        private static async Task QueryInvoices(IContainer container)
        {
            var invoicesService = container.GetInstance<IInvoicesService>();
            var invoices = await invoicesService.GetEvenInvoicesAsync();
            
            foreach (var invoice in invoices)
            {
                System.Console.WriteLine(invoice);
            }
        }
    }
}
