using System.Collections.Generic;
using System.Threading.Tasks;
using UriDi.Console.Models;

namespace UriDi.Console.HttpClients
{
    public class InvoiceHttpClient : IInvoiceHttpClient
    {
        private readonly RegionConfiguration _configuration;

        public InvoiceHttpClient(RegionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<string>> GetInvoicesAsync()
        {
            System.Console.WriteLine($"Query from {_configuration.Email.BaseUrl}:");
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
