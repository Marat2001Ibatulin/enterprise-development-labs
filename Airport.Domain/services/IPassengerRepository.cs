using Airport.Domain.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.services
{
    public interface IPassengerRepository : IRepository<Passenger, int>
    {
        Task<IList<Passenger>> GetPassengersByFlight(string flightCode);
        Task<IList<Passenger>> GetPassengersWithoutBaggage(string flightCode);
        
    }
}
