using System.Threading.Tasks;
using StructureMap;
using UriDi.Domain.Services;

namespace UriDi.Console.Controllers
{
    public class CustomersController
    {
        public async Task GetCustomers(IContainer container)
        {
            var customersService = container.GetInstance<ICustomersService>();
            var customers = await customersService.GetOddCustomersAsync();

            foreach (var customer in customers)
            {
                System.Console.WriteLine(customer);
            }
        }
    }
}
