using Airport.Domain.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.services.Data
{
    public static class DataSeeder
    {
        public static readonly List<Jet> Jets =
        [
            new()
        {
            Model = "Airbus A320",
            LoadCapacity = 18000,
            Productivity = 850,
            MaxPassengers = 180
        },
        new()
        {
            Model = "Boeing 737",
            LoadCapacity = 20000,
            Productivity = 900,
            MaxPassengers = 160
        }
        ];

        public static readonly List<Passenger> Passengers =
        [
            new()
        {
            PassportId = 101,
            FullName = "Иван Иванов"
        },
        new()
        {
            PassportId = 102,
            FullName = "Анна Смирнова"
        },
        new()
        {
            PassportId = 103,
            FullName = "Петр Кузнецов"
        }
        ];

        public static readonly List<RegisteredPassenger> RegisteredPassengers =
        [
            new()
        {
            TicketNumber = 1,
            SeatNumber = 12,
            BaggageWeight = 20,
            Passenger = Passengers[0]
        },
        new()
        {
            TicketNumber = 2,
            SeatNumber = 15,
            BaggageWeight = 25,
            Passenger = Passengers[1]
        },
        new()
        {
            TicketNumber = 3,
            SeatNumber = 18,
            BaggageWeight = 0,
            Passenger = Passengers[2]
        }
        ];

        public static readonly List<Flight> Flights =
        [
            new()
        {
            Code = "SU100",
            DeparturePoint = "Москва",
            ArrivalPoint = "Санкт-Петербург",
            DepartureDate = new DateOnly(2025, 6, 10),
            ArrivalDate = new DateOnly(2025, 6, 10),
            DepartureTime = new TimeOnly(14, 30),
            TravelTime = TimeSpan.FromHours(1.5),
            Jet = Jets[0],
            Passengers = new List<RegisteredPassenger> { RegisteredPassengers[0], RegisteredPassengers[1] }
        },
        new()
        {
            Code = "DP200",
            DeparturePoint = "Казань",
            ArrivalPoint = "Сочи",
            DepartureDate = new DateOnly(2025, 6, 15),
            ArrivalDate = new DateOnly(2025, 6, 15),
            DepartureTime = new TimeOnly(9, 0),
            TravelTime = TimeSpan.FromHours(2.5),
            Jet = Jets[1],
            Passengers = new List<RegisteredPassenger> { RegisteredPassengers[2] }
        }
        ];
    }
}
