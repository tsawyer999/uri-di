using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UriDi.Infrastructure.HttpClients;

namespace UriDi.Domain.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersHttpClient _customersHttpClient;

        public CustomersService(ICustomersHttpClient customersHttpClient)
        {
            _customersHttpClient = customersHttpClient;
        }
        
        public async Task<List<string>> GetOddCustomersAsync()
        {
            var customers = await _customersHttpClient.GetCustomersAsync();

            var prefixLength = "CUSTOMER".Length;

            return customers.Where(c => Convert.ToInt32(c.Substring(prefixLength)) % 2 == 1).ToList();
        }
    }
}
