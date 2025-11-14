using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var eventPlan = new EventPlan(GenerateNewId(), "Sommer");

            Play play01 = new Play(GenerateNewId(), "Hamlet", "Shakespeare", 180);
            Play play02 = new Play(GenerateNewId(), "Faust", "Goethe", 200);

            eventPlan.Plays.Add(play01);
            eventPlan.Plays.Add(play02);

            // Ticket ticket01 = new Ticket(GenerateNewId(), "Zürich Opernhaus 2023",12,1,"Mustermann", DateAndTime.Today, play01);

            var seatingPlan = new SeatingPlan
            {
                Id = GenerateNewId(),
                Seats = new List<Seat>
                {
                    new Seat { SeatNumber = 1, IsAvailable = true },
                    new Seat { SeatNumber = 2, IsAvailable = true },
                    new Seat { SeatNumber = 3, IsAvailable = true },
                    new Seat { SeatNumber = 4, IsAvailable = true },
                    new Seat { SeatNumber = 33, IsAvailable = true },
                    new Seat { SeatNumber = 5, IsAvailable = true },
                    new Seat { SeatNumber = 6, IsAvailable = true },
                    new Seat { SeatNumber = 7, IsAvailable = true },
                }
            };

            Ticket ticket02 = seatingPlan.BuyTicket("Mustermann", 2, play02);
            Ticket ticket03 = seatingPlan.BuyTicket("Mustermann", 2, play02);

            if (ticket02 != null)
            {
                Console.WriteLine(
                $"Ticket for {ticket02.GuestName} bought: {ticket02.Play.Title}, " + $"Price: {ticket02.Price} CHF, Date: {ticket02.SaleDate:d}");
            }
        }
        public static int GenerateNewId()
        {
            return new Random().Next(1000, 9999);
        }
    }
}
