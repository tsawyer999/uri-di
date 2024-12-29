using System.Threading.Tasks;
using UriDi.Console.Configuration;
using UriDi.Console.Controllers;
using UriDi.Models.Configuration;

namespace UriDi.Console
{
    public class Program
    {
        public static async Task Main()
        {
            var configurations = Configuration.Configuration.GetConfiguration();
            var container = ApplicationRegistry.GetContainer(configurations);

            var customersController = new CustomersController(container);
            await customersController.GetCustomers(Region.US);
            
            var invoicesController = new InvoicesController(container);
            await invoicesController.GetInvoices(Region.EU);
            
            System.Console.WriteLine("\n\nPROCESS DONE");
        }
    }
}
