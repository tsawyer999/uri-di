using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Infrastructure.HttpClients
{
    public interface IInvoicesHttpClient
    {
        Task<List<string>> GetInvoicesAsync();
    }
}
