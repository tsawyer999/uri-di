using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Infrastructure.HttpClients
{
    public interface ICustomerHttpClient
    {
        Task<List<string>> GetCustomersAsync();        
    }
}
