using Airport.Domain.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.services
{
    public interface IRegisteredPassengerRepository : IRepository<RegisteredPassenger, int>
    {
        Task<IList<RegisteredPassenger>> GetByFlight(string flightCode);
        Task<IList<RegisteredPassenger>> GetWithoutBaggageByFlight(string flightCode);
    }
}
