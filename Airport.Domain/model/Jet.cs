using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.model
{
    public class Jet
    {
        public string Model { get; set; } = string.Empty;
        public int LoadCapacity { get; set; }
        public int Productivity { get; set; }
        public int MaxPassengers { get; set; }

        public bool IsOverloaded(int currentPassengerCount)
        {
            return currentPassengerCount > MaxPassengers;
        }

        public override string ToString()
        {
            return $"Самолёт: {Model}, {MaxPassengers} пассажиров, {LoadCapacity} кг";
        }
    }
}   
