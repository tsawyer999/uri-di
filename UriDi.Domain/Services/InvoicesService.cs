using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UriDi.Infrastructure.HttpClients;

namespace UriDi.Domain.Services
{
    public class InvoicesService : IInvoicesService
    {
        private readonly IInvoicesHttpClient _invoicesHttpClient;

        public InvoicesService(IInvoicesHttpClient invoicesHttpClient)
        {
            _invoicesHttpClient = invoicesHttpClient;
        }
        
        public async Task<List<string>> GetEvenInvoicesAsync()
        {
            var invoices = await _invoicesHttpClient.GetInvoicesAsync();

            var prefixLength = "INVOICE".Length;

            return invoices.Where(c => Convert.ToInt32(c.Substring(prefixLength)) % 2 == 0).ToList();
        }
    }
}
