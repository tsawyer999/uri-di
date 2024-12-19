using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Console.HttpClients
{
    public class CustomerHttpClient : ICustomerHttpClient
    {
        public async Task<List<string>> GetCustomersAsync()
        {
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
