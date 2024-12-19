using System.Collections.Generic;
using System.Threading.Tasks;
using UriDi.Console.Models;

namespace UriDi.Console.HttpClients
{
    public class CustomerHttpClient : ICustomerHttpClient
    {
        private readonly RegionConfiguration _configuration;

        public CustomerHttpClient(RegionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<string>> GetCustomersAsync()
        {
            System.Console.WriteLine(_configuration.Email.ApiKey);
            return await Task.Run(() =>
            {
                return new List<string>
                {
                    "CUSTOMER1",
                    "CUSTOMER2",
                    "CUSTOMER3",
                    "CUSTOMER4",
                    "CUSTOMER5"
                };
            });
        }
    }
}
