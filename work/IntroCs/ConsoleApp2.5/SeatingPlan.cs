using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._5
{
    public class SeatingPlan
    {
        public int Id { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        public Ticket BuyTicket(string userName, int amount, Play play)
        {
            var availableSeats = Seats.Where(s => s.IsAvailable).ToList();
           

            if (availableSeats.Count < amount)
            {
                throw new ArgumentException("Not enough seats available.");
            }

            var selectedSeats = availableSeats.Take(amount).ToList();
            var seatNumbers = new List<int>();
            var seatTypes = new List<SeatType>();
            var totalPrice = 0;

            foreach (var seat in selectedSeats)
            {
                seat.IsAvailable = false;
                seatNumbers.Add(seat.SeatNumber);
                seatTypes.Add(seat.Type);

                switch (seat.Type)
                {
                    case SeatType.REGULAR: totalPrice += 51; break;
                    case SeatType.BALCONY: totalPrice += 80; break;
                    case SeatType.WHEELCHAIR: totalPrice += 30; break;
                    default: break;
                }
            }

            var ticket = new Ticket(
                GenerateNewId(),
                $"Ticket für {play.Title}",
                totalPrice,
                seatNumbers,
                seatTypes,
                userName,
                DateTime.Now,
                play
            );

            Tickets.Add(ticket);
            return ticket;
        }

        // Hilfmethode
        private static int GenerateNewId()
        {
            return new Random().Next(1000, 9999);
        }

    }
}