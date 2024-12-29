using System.Threading.Tasks;
using StructureMap;
using UriDi.Domain.Services;

namespace UriDi.Console.Controllers
{
    public class InvoicesController
    {
        private readonly IContainer _container;

        public InvoicesController(IContainer container)
        {
            _container = container;
        }

        public async Task GetInvoices(string region)
        {
            var invoicesService = _container.GetInstance<IInvoicesService>(region);
            var invoices = await invoicesService.GetEvenInvoicesAsync();
            
            foreach (var invoice in invoices)
            {
                System.Console.WriteLine(invoice);
            }
        }
    }
}
