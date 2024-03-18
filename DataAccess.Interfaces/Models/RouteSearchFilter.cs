namespace ProviderTwo.DataAccess.Interfaces.Models
{
    public class RouteSearchFilter
    {
        // Mandatory
        // Start point of route, e.g. Moscow 
        public string Origin { get; set; }

        // Mandatory
        // End point of route, e.g. Sochi
        public string Destination { get; set; }

        // Mandatory
        // Start date of route
        public DateTime OriginDate { get; set; }

        // Optional
        // Minimum value of timelimit for route
        public DateTime? MinTimeLimit { get; set; }
    }
}
