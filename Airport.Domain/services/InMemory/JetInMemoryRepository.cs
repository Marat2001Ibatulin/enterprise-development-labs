using Airport.Domain.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.services.InMemory
{
    public class JetInMemoryRepository : IRepository<Jet, string>
    {
        private List<Jet> _jets;

        public JetInMemoryRepository()
        {
            _jets = new List<Jet>();
        }

        public Task<Jet> Add(Jet entity)
        {
            _jets.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<bool> Delete(string key)
        {
            var entity = _jets.FirstOrDefault(j => j.Model == key);
            if (entity == null) return Task.FromResult(false);
            _jets.Remove(entity);
            return Task.FromResult(true);
        }

        public Task<Jet?> Get(string key) =>
            Task.FromResult(_jets.FirstOrDefault(j => j.Model == key));

        public Task<IList<Jet>> GetAll() =>
            Task.FromResult((IList<Jet>)_jets);

        public Task<Jet> Update(Jet entity)
        {
            var index = _jets.FindIndex(j => j.Model == entity.Model);
            if (index == -1) throw new InvalidOperationException("Jet not found");
            _jets[index] = entity;
            return Task.FromResult(entity);
        }
    }
}
