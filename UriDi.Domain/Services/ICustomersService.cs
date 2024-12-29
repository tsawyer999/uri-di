using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Domain.Services
{
    public interface ICustomersService
    {
        Task<List<string>> GetOddCustomersAsync();
    }
}
