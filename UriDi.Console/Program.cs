using System.Threading.Tasks;
using UriDi.Console.Controllers;
using UriDi.Models.Configuration;

namespace UriDi.Console
{
    public class Program
    {
        public static async Task Main()
        {
            var configurations = Configuration.GetConfiguration();
            var container = ApplicationRegistry.GetContainer(configurations);

            var customersController = new CustomersController();
            await customersController.GetCustomers(container.GetProfile(Region.US));
            
            var invoicesController = new InvoicesController();
            await invoicesController.GetInvoices(container.GetProfile(Region.EU));
            
            System.Console.WriteLine("\n\nPROCESS DONE");
        }
    }
}
