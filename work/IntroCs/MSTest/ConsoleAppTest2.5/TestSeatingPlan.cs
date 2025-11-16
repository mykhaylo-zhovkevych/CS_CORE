using ConsoleApp2._5;

namespace ConsoleAppTest2._5
{
    [TestClass]
    public class TestSeatingPlan
    {

        private EventPlan _eventPlan;
        private Play _play01;
        private Play _play02;
        private SeatingPlan _seatingPlan;

        [TestInitialize]
        public void Setup()
        {
            _eventPlan = new EventPlan(1, "Test Event Plan");
            _play01 = new Play(1, "Hamlet", "Shakespeare", 120);
            _play02 = new Play(2, "Faust", "Goethe", 150);
            _seatingPlan = new SeatingPlan
            {
                Id = 1,
                Seats = new List<Seat>
                {
                    new Seat { SeatNumber = 1, Type = SeatType.REGULAR, IsAvailable = true },
                    new Seat { SeatNumber = 2, Type = SeatType.BALCONY, IsAvailable = true },
                    new Seat { SeatNumber = 3, Type = SeatType.WHEELCHAIR, IsAvailable = true },
                    new Seat { SeatNumber = 4, Type = SeatType.REGULAR, IsAvailable = true },
                }
            };
        }


        [TestMethod]
        public void TestBuyTicket_AddsTicketsForDifferentUsers()
        {
            _seatingPlan.BuyTicket("TestUser01", 1, _play02);
            _seatingPlan.BuyTicket("TestUser02", 1, _play01);

            Assert.IsTrue(_seatingPlan.Tickets.Any(t => t.GuestName == "TestUser01"));
            Assert.IsTrue(_seatingPlan.Tickets.Any(t => t.GuestName == "TestUser02"));
            Assert.AreEqual(2, _seatingPlan.Tickets.Count);
        }


        [TestMethod]
        public void TestBuyTicket_IfCorrectPriceCalculated()
        {
            // Arrange
            var ticket = _seatingPlan.BuyTicket("TestUser03", 3, _play02);

            // Assert
            Assert.IsTrue(ticket.Price == (51 + 80 + 30) && ticket.GuestName == "TestUser03");  

        }

        [TestMethod]
        public void TestBuyTicket_NotEnoughSeatsAvailable_ThrowsException()
        {
            // Assert & Act
            Assert.ThrowsException<ArgumentException>(() =>
            {
                _seatingPlan.BuyTicket("TestUser03", 5, _play01);
            });
        }

        [TestMethod]
        public void TestBuyTicket_SeatTypesMatchSelectedSeats()
        {
            // Arrange & Act
            var ticket = _seatingPlan.BuyTicket("TestUser04", 2, _play01);

            var expected = _seatingPlan.Seats
                .Where(s => !s.IsAvailable)
                .Select(s => s.Type)
                .ToList();

            // Assert
            CollectionAssert.AreEqual(expected, ticket.SeatTypes);
        }

    }
}
