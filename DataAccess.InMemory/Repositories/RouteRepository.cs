using Microsoft.EntityFrameworkCore;

using ProviderTwo.DataAccess.InMemory.Mapping;
using ProviderTwo.DataAccess.Interfaces;
using ProviderTwo.DataAccess.Interfaces.Models;
using ProviderTwo.Entities.Models;

namespace ProviderTwo.DataAccess.InMemory.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly ProviderTwoDbContext _context;

        public RouteRepository(ProviderTwoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Route>> Search(RouteSearchFilter filter)
        {
            var query = GetQuery(filter);

            var entities = await query.ToListAsync();

            var items = entities.Select(RouteMappings.ToDomainModel).ToList();

            return items;
        }

        private IQueryable<Entities.Route> GetQuery(RouteSearchFilter filter)
        {
            var query = _context.Routes.AsQueryable();

            query = query.Where(x => x.OriginDateTime.Date == filter.OriginDate.Date);

            if (filter.MinTimeLimit.HasValue)
            {
                query = query.Where(x => x.TimeLimit >= filter.MinTimeLimit.Value);
            }

            if (!string.IsNullOrEmpty(filter.Origin))
            {
                query = query.Where(x => x.Origin.ToUpper().Contains(filter.Origin.ToUpper()));
            }

            if (!string.IsNullOrEmpty(filter.Destination))
            {
                query = query.Where(x => x.Destination.ToUpper().Contains(filter.Destination.ToUpper()));
            }

            return query;
        }
    }
}
