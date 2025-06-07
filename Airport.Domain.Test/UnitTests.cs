using Airport.Domain.model;
using Airport.Domain.services.InMemory;

namespace Airport.Domain.Test
{
    public class Test1
    {
        [Fact]
        public async Task GetFlightsByRoute_ReturnsCorrectFlights()
        {

            var repo = new FlightInMemoryRepository();


            var flights = await repo.GetFlightsByRoute("Moscow", "London");


            Assert.NotNull(flights);

        }


    }

    public class Test2
    {
        [Fact]
        public async Task GetPassengersWithoutBaggageSortedByName_ReturnsCorrectPassengers()
        {

            var repo = new FlightInMemoryRepository();


            var flight = new Flight
            {
                Code = "AB123",
                Passengers = new List<RegisteredPassenger>
                {
                    new RegisteredPassenger { TicketNumber = 1, SeatNumber = 1, BaggageWeight = 0, Passenger = new Passenger { FullName = "Иван Иванов", PassportId = 101 } },
                    new RegisteredPassenger { TicketNumber = 2, SeatNumber = 2, BaggageWeight = 10, Passenger = new Passenger { FullName = "Петр Петров", PassportId = 102 } },
                    new RegisteredPassenger { TicketNumber = 3, SeatNumber = 3, BaggageWeight = 0, Passenger = new Passenger { FullName = "Алексей Алексеев", PassportId = 103 } }
                }
            };


            await repo.Add(flight);


            var passengers = await repo.GetPassengersWithoutBaggageSortedByName("AB123");


            Assert.NotNull(passengers);
            Assert.Equal(2, passengers.Count);


            Assert.Equal("Алексей Алексеев", passengers[0].Passenger.FullName);
            Assert.Equal("Иван Иванов", passengers[1].Passenger.FullName);
        }
    }

    public class Test3
    {
        [Fact]
        public async Task GetFlightsByJetModelAndPeriod_ReturnsCorrectFlights()
        {
            
            var repo = new FlightInMemoryRepository();

            var jet1 = new Jet { Model = "Boeing 737", LoadCapacity = 20000, MaxPassengers = 150 };
            var jet2 = new Jet { Model = "Airbus A320", LoadCapacity = 22000, MaxPassengers = 160 };

            var flight1 = new Flight
            {
                Code = "FL001",
                DepartureDate = new DateOnly(2025, 6, 1),
                Jet = jet1
            };

            var flight2 = new Flight
            {
                Code = "FL002",
                DepartureDate = new DateOnly(2025, 6, 5),
                Jet = jet1
            };

            var flight3 = new Flight
            {
                Code = "FL003",
                DepartureDate = new DateOnly(2025, 7, 10),
                Jet = jet2
            };

            await repo.Add(flight1);
            await repo.Add(flight2);
            await repo.Add(flight3);

            var from = new DateOnly(2025, 6, 1);
            var to = new DateOnly(2025, 6, 30);

            
            var flights = await repo.GetFlightsByJetModelAndPeriod("Boeing 737", from, to);

            
            Assert.NotNull(flights);
            Assert.Equal(2, flights.Count);
            Assert.All(flights, f => Assert.Equal("Boeing 737", f.Jet.Model));
            Assert.All(flights, f => Assert.InRange(f.DepartureDate, from, to));
        }

    }
    public class Test4
    {
        [Fact]
        public async Task GetTop5FlightsByPassengerCount_ReturnsTop5()
        {
            
            var repo = new FlightInMemoryRepository();

            for (int i = 1; i <= 10; i++)
            {
                var flight = new Flight
                {
                    Code = $"FL{i:D3}",
                    Passengers = new List<RegisteredPassenger>()
                };

                
                for (int p = 0; p < i * 3; p++)
                {
                    flight.Passengers.Add(new RegisteredPassenger
                    {
                        TicketNumber = p,
                        Passenger = new Passenger { PassportId = p, FullName = $"Passenger {p}" }
                    });
                }

                await repo.Add(flight);
            }

           
            var topFlights = await repo.GetTop5FlightsByPassengerCount();

            
            Assert.NotNull(topFlights);
            Assert.Equal(5, topFlights.Count);

           
            for (int i = 0; i < topFlights.Count - 1; i++)
            {
                Assert.True(topFlights[i].PassengerCount >= topFlights[i + 1].PassengerCount);
            }

            
            Assert.Equal("FL010", topFlights[0].Code);
            Assert.Equal(30, topFlights[0].PassengerCount);
        }
    }
    public class Test5
    {
        [Fact]
        public async Task GetFlightsWithMinimalTravelTime_ReturnsAllWithMinTravelTime()
        {
            
            var repo = new FlightInMemoryRepository();

            
            var flights = new List<Flight>
            {
                new Flight { Code = "FL001", TravelTime = TimeSpan.FromHours(3) },
                new Flight { Code = "FL002", TravelTime = TimeSpan.FromHours(2) }, 
                new Flight { Code = "FL003", TravelTime = TimeSpan.FromHours(4) },
                new Flight { Code = "FL004", TravelTime = TimeSpan.FromHours(2) }, 
                new Flight { Code = "FL005", TravelTime = TimeSpan.FromHours(5) }
            };

            foreach (var flight in flights)
            {
                await repo.Add(flight);
            }

            
            var minimalFlights = await repo.GetFlightsWithMinimalTravelTime();

            
            Assert.NotNull(minimalFlights);
            Assert.Equal(2, minimalFlights.Count);

            
            foreach (var flight in minimalFlights)
            {
                Assert.Equal(TimeSpan.FromHours(2), flight.TravelTime);
            }
        }
    }

    public class Test6
    {
        [Fact]
        public async Task GetAverageAndMaxLoadFromDeparture_ReturnsCorrectValues()
        {
            
            var repo = new FlightInMemoryRepository();

            var flights = new List<Flight>
            {
                new Flight { Code = "FL001", DeparturePoint = "Moscow", Passengers = new List<RegisteredPassenger> { new RegisteredPassenger(), new RegisteredPassenger() } }, 
                new Flight { Code = "FL002", DeparturePoint = "Moscow", Passengers = new List<RegisteredPassenger> { new RegisteredPassenger() } }, 
                new Flight { Code = "FL003", DeparturePoint = "Moscow", Passengers = new List<RegisteredPassenger> { new RegisteredPassenger(), new RegisteredPassenger(), new RegisteredPassenger() } }, 
                new Flight { Code = "FL004", DeparturePoint = "New York", Passengers = new List<RegisteredPassenger> { new RegisteredPassenger() } } 
            };

            foreach (var flight in flights)
            {
                await repo.Add(flight);
            }

            
            var (averageLoad, maxLoad) = await repo.GetAverageAndMaxLoadFromDeparture("Moscow");

            
            Assert.Equal(2.0, averageLoad);  
            Assert.Equal(3, maxLoad);
        }
    }













}