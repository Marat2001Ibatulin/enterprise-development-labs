using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.model
{
    public class Flight
    {
        public string Code { get; set; } = string.Empty;
        public string DeparturePoint { get; set; } = string.Empty;
        public string ArrivalPoint { get; set; } = string.Empty;
        public DateOnly DepartureDate { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public TimeOnly DepartureTime { get; set; }
        public TimeSpan TravelTime { get; set; }
        public Jet Jet { get; set; } = new();
        public List<RegisteredPassenger> Passengers { get; set; } = new();

        public int PassengerCount => Passengers.Count;

        public int TotalBaggageWeight()
        {
            return Passengers.Sum(p => p.BaggageWeight);
        }

        public bool IsBetweenDates(DateOnly from, DateOnly to)
        {
            return DepartureDate >= from && DepartureDate <= to;
        }

        public void AddPassenger(RegisteredPassenger registeredPassenger)
        {
            Passengers.Add(registeredPassenger);
        }

        public override string ToString()
        {
            return $"Рейс {Code}: {DeparturePoint} → {ArrivalPoint}, {DepartureDate}, Пассажиров: {PassengerCount}";
        }
    }




}

