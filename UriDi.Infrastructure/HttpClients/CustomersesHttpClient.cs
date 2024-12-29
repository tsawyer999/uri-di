using System.Collections.Generic;
using System.Threading.Tasks;
using UriDi.Models.Configuration;

namespace UriDi.Infrastructure.HttpClients
{
    public class CustomersesHttpClient : ICustomersHttpClient
    {
        private readonly RegionConfiguration _configuration;

        public CustomersesHttpClient(RegionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<string>> GetCustomersAsync()
        {
            System.Console.WriteLine($"Query from {_configuration.Email.BaseUrl}:");
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
