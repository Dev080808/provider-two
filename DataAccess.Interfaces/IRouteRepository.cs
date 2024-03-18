using ProviderTwo.DataAccess.Interfaces.Models;
using ProviderTwo.Entities.Models;

namespace ProviderTwo.DataAccess.Interfaces
{
    public interface IRouteRepository
    {
        Task<List<Route>> Search(RouteSearchFilter filter);
    }
}
