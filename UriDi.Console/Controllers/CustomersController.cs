using System.Threading.Tasks;
using StructureMap;
using UriDi.Domain.Services;

namespace UriDi.Console.Controllers
{
    public class CustomersController
    {
        private readonly IContainer _container;

        public CustomersController(IContainer container)
        {
            _container = container;
        }
        
        public async Task GetCustomers(string region)
        {
            var customersService = _container.GetInstance<ICustomersService>(region);
            var customers = await customersService.GetOddCustomersAsync();

            foreach (var customer in customers)
            {
                System.Console.WriteLine(customer);
            }
        }
    }
}
