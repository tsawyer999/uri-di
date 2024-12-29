using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Domain.Services
{
    public interface IInvoicesService
    {
        Task<List<string>> GetEvenInvoicesAsync();
    }
}
