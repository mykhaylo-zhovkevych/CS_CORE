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
        public void TestBuyTicket_IfIsCorrect()
        {

            _seatingPlan.BuyTicket("TestUser01", 2, _play02);
            _seatingPlan.BuyTicket("TestUser02", 2, _play01);

            Assert.IsTrue(_seatingPlan.Tickets.Any(ticket => ticket.GuestName == "TestUser01" && ticket.GuestName == "TestUser02") );

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
 
            Assert.ThrowsException<ArgumentException>(() =>
            {
                _seatingPlan.BuyTicket("TestUser03", 5, _play01);
            });
        }


    }
}
