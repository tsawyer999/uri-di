using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Infrastructure.HttpClients
{
    public interface ICustomersHttpClient
    {
        Task<List<string>> GetCustomersAsync();        
    }
}
