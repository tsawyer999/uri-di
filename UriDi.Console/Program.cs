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
        private static readonly string[] _regions = {
            "ca",
            "us",
            "eu"
        };

        public static void Main()
        {
            var configuration = GetConfiguration();
            var configurations = new Dictionary<string, RegionConfiguration>();

            foreach (var region in _regions)
            {
                var regionConfiguration = new RegionConfiguration();
                configuration.GetSection(region).Bind(regionConfiguration);
                configurations.Add(region, regionConfiguration);
            }

            var container = CreateContainer(configurations.First().Value);
            Task.WaitAll(
                DisplayCustomers(container),
                DisplayInvoices(container)
            );
            System.Console.WriteLine("PROCESS DONE");
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            return configuration;
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
        
        private static async Task DisplayCustomers(Container container)
        {
            var customersService = container.GetInstance<ICustomersService>();
            var customers = await customersService.GetOddCustomersAsync();
            foreach (var customer in customers)
            {
                System.Console.WriteLine(customer);
            }
        }

        private static async Task DisplayInvoices(Container container)
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
