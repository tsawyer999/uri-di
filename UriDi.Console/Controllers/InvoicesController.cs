using System.Threading.Tasks;
using StructureMap;
using UriDi.Domain.Services;

namespace UriDi.Console.Controllers
{
    public class InvoicesController
    {
        public async Task GetInvoices(IContainer container)
        {
            var invoicesService = container.GetInstance<IInvoicesService>();
            var invoices = await invoicesService.GetEvenInvoicesAsync();
            
            foreach (var invoice in invoices)
            {
                System.Console.WriteLine(invoice);
            }
        }
    }
}
