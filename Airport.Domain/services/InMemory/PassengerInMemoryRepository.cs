using Airport.Domain.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.services.InMemory
{
    public class PassengerInMemoryRepository : IRepository<Passenger, int>
    {
        private List<Passenger> _passengers;

        public PassengerInMemoryRepository()
        {
            _passengers = new List<Passenger>();
        }

        public Task<Passenger> Add(Passenger entity)
        {
            _passengers.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<bool> Delete(int key)
        {
            var entity = _passengers.FirstOrDefault(p => p.PassportId == key);
            if (entity == null) return Task.FromResult(false);
            _passengers.Remove(entity);
            return Task.FromResult(true);
        }

        public Task<Passenger?> Get(int key) =>
            Task.FromResult(_passengers.FirstOrDefault(p => p.PassportId == key));

        public Task<IList<Passenger>> GetAll() =>
            Task.FromResult((IList<Passenger>)_passengers);

        public Task<Passenger> Update(Passenger entity)
        {
            var index = _passengers.FindIndex(p => p.PassportId == entity.PassportId);
            if (index == -1) throw new InvalidOperationException("Passenger not found");
            _passengers[index] = entity;
            return Task.FromResult(entity);
        }
    }
}
