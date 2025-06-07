using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.model
{
    public class Passenger
    {
        public int PassportId { get; set; }
        public string FullName { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{FullName} (паспорт {PassportId})";
        }




    }
}
