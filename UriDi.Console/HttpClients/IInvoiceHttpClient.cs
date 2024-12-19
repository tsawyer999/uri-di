using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Console.HttpClients
{
    public interface IInvoiceHttpClient
    {
        Task<List<string>> GetInvoicesAsync();
    }
}
