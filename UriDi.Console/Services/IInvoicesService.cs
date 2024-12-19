using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Console.Services
{
    public interface IInvoicesService
    {
        Task<List<string>> GetEvenInvoicesAsync();
    }
}
