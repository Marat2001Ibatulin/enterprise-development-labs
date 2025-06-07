using Airport.Domain.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.services.InMemory
{
    public class FlightInMemoryRepository : IRepository<Flight, string>
    {
        private List<Flight> _flights;

        public FlightInMemoryRepository()
        {
            _flights = new List<Flight>();
        }

        public Task<Flight> Add(Flight entity)
        {
            _flights.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<bool> Delete(string key)
        {
            var entity = _flights.FirstOrDefault(f => f.Code == key);
            if (entity == null) return Task.FromResult(false);
            _flights.Remove(entity);
            return Task.FromResult(true);
        }

        public Task<Flight?> Get(string key) =>
            Task.FromResult(_flights.FirstOrDefault(f => f.Code == key));

        public Task<IList<Flight>> GetAll() =>
            Task.FromResult((IList<Flight>)_flights);

        public Task<Flight> Update(Flight entity)
        {
            var index = _flights.FindIndex(f => f.Code == entity.Code);
            if (index == -1) throw new InvalidOperationException("Flight not found");
            _flights[index] = entity;
            return Task.FromResult(entity);
        }
        public Task<IList<Flight>> GetFlightsByRoute(string departure, string arrival)
        {
            var result = _flights
                .Where(f => f.DeparturePoint.Equals(departure, StringComparison.OrdinalIgnoreCase)
                         && f.ArrivalPoint.Equals(arrival, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Task.FromResult((IList<Flight>)result);
        }

        public async Task<IList<RegisteredPassenger>> GetPassengersWithoutBaggageSortedByName(string flightCode)
        {
            var flight = await Get(flightCode);
            if (flight == null)
                return new List<RegisteredPassenger>();

            return flight.Passengers
                .Where(p => p.BaggageWeight == 0)
                .OrderBy(p => p.Passenger.FullName)
                .ToList();
        }
        public async Task<IList<Flight>> GetFlightsByJetModelAndPeriod(string jetModel, DateOnly from, DateOnly to)
        {
            var result = _flights
                .Where(f => f.Jet.Model == jetModel && f.DepartureDate >= from && f.DepartureDate <= to)
                .ToList();

            return await Task.FromResult(result);
        }

        public async Task<IList<Flight>> GetTop5FlightsByPassengerCount()
        {
            var top5 = _flights
                .OrderByDescending(f => f.PassengerCount)
                .Take(5)
                .ToList();

            return await Task.FromResult(top5);

        }

        public async Task<IList<Flight>> GetFlightsWithMinimalTravelTime()
        {
            if (!_flights.Any())
                return new List<Flight>();

            var minTravelTime = _flights.Min(f => f.TravelTime);
            var result = _flights.Where(f => f.TravelTime == minTravelTime).ToList();

            return await Task.FromResult(result);
        }

        public async Task<(double AverageLoad, int MaxLoad)> GetAverageAndMaxLoadFromDeparture(string departurePoint)
        {
            var flightsFromPoint = _flights.Where(f => f.DeparturePoint == departurePoint).ToList();

            if (!flightsFromPoint.Any())
                return (0, 0);

            double averageLoad = flightsFromPoint.Average(f => f.PassengerCount);
            int maxLoad = flightsFromPoint.Max(f => f.PassengerCount);

            return await Task.FromResult((averageLoad, maxLoad));
        }

    }
}
