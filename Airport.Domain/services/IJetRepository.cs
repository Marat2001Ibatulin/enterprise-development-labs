using Airport.Domain.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.services
{
    public interface IJetRepository : IRepository<Jet, string>
    {
        Task<IList<Jet>> GetJetsByModel(string model);
        
    }
}
