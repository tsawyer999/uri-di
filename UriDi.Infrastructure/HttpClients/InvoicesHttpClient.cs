using System.Collections.Generic;
using System.Threading.Tasks;
using UriDi.Models.Configuration;

namespace UriDi.Infrastructure.HttpClients
{
    public class InvoicesHttpClient : IInvoicesHttpClient
    {
        private readonly RegionConfiguration _configuration;

        public InvoicesHttpClient(RegionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<List<string>> GetInvoicesAsync()
        {
            System.Console.WriteLine($"Query from {_configuration.Email.BaseUrl}:");
            return Task.Run(() =>
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
