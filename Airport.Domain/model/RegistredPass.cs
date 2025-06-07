using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.model
{
    public class RegisteredPassenger
    {
        public int TicketNumber { get; set; }
        public int SeatNumber { get; set; }
        public int BaggageWeight { get; set; }
        public Passenger Passenger { get; set; } = new();

        public bool HasNoBaggage()
        {
            return BaggageWeight == 0;
        }

        public override string ToString()
        {
            return $"Билет {TicketNumber}, место {SeatNumber}, багаж: {BaggageWeight} кг, пассажир: {Passenger.FullName}";
        }
    }

}
