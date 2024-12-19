using System.Collections.Generic;
using System.Threading.Tasks;

namespace UriDi.Console.Services
{
    public interface ICustomersService
    {
        Task<List<string>> GetOddCustomersAsync();
    }
}
