using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UriDi.Console.HttpClients;

namespace UriDi.Console.Services
{
    public class InvoicesService : IInvoicesService
    {
        private readonly IInvoiceHttpClient _invoiceHttpClient;

        public InvoicesService(IInvoiceHttpClient invoiceHttpClient)
        {
            _invoiceHttpClient = invoiceHttpClient;
        }
        
        public async Task<List<string>> GetEvenInvoicesAsync()
        {
            var invoices = await _invoiceHttpClient.GetInvoicesAsync();

            var prefixLength = "INVOICE".Length;

            return invoices.Where(c => Convert.ToInt32(c.Substring(prefixLength)) % 2 == 0).ToList();
        }
    }
}
