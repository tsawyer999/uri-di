using System.Threading.Tasks;
using StructureMap;
using UriDi.Console.HttpClients;
using UriDi.Console.Services;

namespace UriDi.Console
{
    public class Program
    {
        public static void Main()
        {
            var container = CreateContainer();
            
            Task.WaitAll(
                DisplayCustomers(container),
                DisplayInvoices(container)
            );
            System.Console.WriteLine("PROCESS DONE");
        }

        private static Container CreateContainer()
        {
            return new Container(configuration =>
            {
                configuration.For<ICustomerHttpClient>().Use<CustomerHttpClient>();
                configuration.For<IInvoiceHttpClient>().Use<InvoiceHttpClient>();

                configuration.For<ICustomersService>().Use<CustomersService>();
                configuration.For<IInvoicesService>().Use<InvoicesService>();
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
