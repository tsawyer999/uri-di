using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StructureMap;
using UriDi.Console.HttpClients;
using UriDi.Console.Models;
using UriDi.Console.Services;

namespace UriDi.Console
{
    public class Program
    {
        private const string CA = "ca";
        private const string US = "us";
        private const string EU = "eu";
        
        private static readonly string[] Regions = {
            CA,
            US,
            EU
        };

        public static async Task Main()
        {
            var configurations = GetConfiguration();
            var container = CreateContainer(configurations.First().Value);

            await QueryCustomers(container, US);
            await QueryInvoices(container, EU);
            
            System.Console.WriteLine("\n\nPROCESS DONE");
        }

        private static Dictionary<string, RegionConfiguration> GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();
            
            var configurations = new Dictionary<string, RegionConfiguration>();

            foreach (var region in Regions)
            {
                var regionConfiguration = new RegionConfiguration();
                configuration.GetSection(region).Bind(regionConfiguration);
                configurations.Add(region, regionConfiguration);
            }

            return configurations;
        }

        private static Container CreateContainer(RegionConfiguration configuration)
        {
            return new Container(containerConfiguration =>
            {
                containerConfiguration
                    .For<ICustomerHttpClient>()
                    .Use<CustomerHttpClient>()
                    .Ctor<RegionConfiguration>("configuration")
                    .Is(configuration);
                
                containerConfiguration
                    .For<IInvoiceHttpClient>()
                    .Use<InvoiceHttpClient>()
                    .Ctor<RegionConfiguration>("configuration")
                    .Is(configuration);

                containerConfiguration
                    .For<ICustomersService>()
                    .Use<CustomersService>();

                containerConfiguration
                    .For<IInvoicesService>()
                    .Use<InvoicesService>();
            });
        }
        
        private static async Task QueryCustomers(Container container, string region)
        {
            System.Console.WriteLine($"\nRequest for region {region}");

            var customersService = container.GetInstance<ICustomersService>();
            var customers = await customersService.GetOddCustomersAsync();

            foreach (var customer in customers)
            {
                System.Console.WriteLine(customer);
            }
        }

        private static async Task QueryInvoices(Container container, string region)
        {
            System.Console.WriteLine($"\nRequest for region {region}");

            var invoicesService = container.GetInstance<IInvoicesService>();
            var invoices = await invoicesService.GetEvenInvoicesAsync();
            
            foreach (var invoice in invoices)
            {
                System.Console.WriteLine(invoice);
            }
        }
    }
}
