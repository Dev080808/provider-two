using ProviderTwo.DataAccess.Interfaces.Models;
using ProviderTwo.Entities.Models;

using SystemAggregator.Clients.ProviderTwo.Models;

namespace ProviderTwo.UseCases.Mapping
{
    public static class SearchMapping
    {
        public static RouteSearchFilter ToFilter(this ProviderTwoSearchRequest request)
        {
            return new RouteSearchFilter
            {
                Origin = request.Departure,
                Destination = request.Arrival,
                OriginDate = request.DepartureDate,
                MinTimeLimit = request.MinTimeLimit
            };
        }

        public static ProviderTwoRoute ToProviderOnFormat(this Route route)
        {
            return new ProviderTwoRoute
            {
                Id = route.Id,
                Departure = new ProviderTwoPoint()
                {
                    Point = route.Origin,
                    Date = route.OriginDateTime
                },
                Arrival = new ProviderTwoPoint()
                {
                    Point = route.Destination,
                    Date = route.DestinationDateTime
                },
                Price = route.Price,
                TimeLimit = route.TimeLimit,
            };
        }
    }
}
