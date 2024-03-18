using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using SystemAggregator.Clients.ProviderTwo.Constatnts;
using SystemAggregator.Clients.ProviderTwo.Models;
using SystemAggregator.Core.Extensions;
using SystemAggregator.Services;
using SystemAggregator.Services.Mapping;

namespace ProviderTwo.Site.Controllers.Api
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/search")]
    [SwaggerTag("Search API")]
    public class SearchController : ControllerBase
    {
        private readonly Func<string, ISearchService?> _getSearchService;

        public SearchController(Func<string, ISearchService?> getSearchService)
        {
            _getSearchService = getSearchService;
        }

        [HttpPost]
        public async Task<ActionResult<ProviderTwoSearchResponse>> Post(ProviderTwoSearchRequest request)
        {
            var service = _getSearchService(SearchCodes.Route);

            if (service == null)
            {
                return NotFound();
            }

            var result = await service.Search(request.ToCamelCaseJsonElement());

            return result.Code.IsValid()
                ? Ok(result)
                : BadRequest(result);
        }
    }
}