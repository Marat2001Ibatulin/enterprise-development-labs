using Airport.Domain.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.services
{
    public interface IFlightRepository : IRepository<Flight, string>
    {
        Task<IList<Flight>> GetByRoute(string from, string to);
        Task<IList<Flight>> GetByJetModel(string jetModel);
        Task<IList<Flight>> GetFlightsInPeriod(DateOnly from, DateOnly to);
        Task<IList<Flight>> GetTop5ByPassengerCount();
        Task<IList<Flight>> GetFlightsWithMinimalFlightTime();
        Task<(double AverageLoad, int MaxLoad)> GetLoadStatsByDeparture(string departure);
    }
}
