using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Console.HttpClients
{
    public interface ICustomerHttpClient
    {
        Task<List<string>> GetCustomersAsync();        
    }
}
