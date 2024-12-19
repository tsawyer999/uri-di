using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Console.HttpClients
{
    public class InvoiceHttpClient : IInvoiceHttpClient
    {
        public async Task<List<string>> GetInvoicesAsync()
        {
            return await Task.Run(() =>
            {
                return new List<string>
                {
                    "INVOICE1",
                    "INVOICE2",
                    "INVOICE3",
                    "INVOICE4",
                    "INVOICE5"
                };
            });
        }
    }
}
