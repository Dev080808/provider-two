using ProviderTwo.DataAccess.Interfaces;
using ProviderTwo.UseCases.Mapping;

using SystemAggregator.Clients.ProviderTwo.Constatnts;
using SystemAggregator.Clients.ProviderTwo.Models;
using SystemAggregator.Core.Extensions;
using SystemAggregator.Services.SearchService;

namespace ProviderTwo.UseCases.Services
{
    public class RouteSearchService :
        SearchService<ProviderTwoSearchRequest, ProviderTwoSearchResponse>
    {
        private readonly IRouteRepository _repo;

        public RouteSearchService(IRouteRepository repo)
        {
            _repo = repo;
        }

        public override string SearchCode => SearchCodes.Route;

        protected override async Task<ProviderTwoSearchResponse> InvokeSearch(ProviderTwoSearchRequest param)
        {
            var filter = param.ToFilter();
            var routes = await _repo.Search(filter);

            return new ProviderTwoSearchResponse()
            {
                Routes = routes.IsEmpty()
                    ? Array.Empty<ProviderTwoRoute>()
                    : routes.Select(SearchMapping.ToProviderOnFormat).ToArray()
            };
        }
    }
}
