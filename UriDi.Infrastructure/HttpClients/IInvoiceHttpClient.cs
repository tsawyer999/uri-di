using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Infrastructure.HttpClients
{
    public interface IInvoiceHttpClient
    {
        Task<List<string>> GetInvoicesAsync();
    }
}
