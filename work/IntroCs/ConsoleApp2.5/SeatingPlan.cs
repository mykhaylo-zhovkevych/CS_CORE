using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._5
{
    internal class SeatingPlan
    {
        public int Id { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();

        public Ticket BuyTicket(string UserName, int amount, Play play)
        {
            var availableSeats = Seats.Where(s => s.IsAvailable).Take(amount).ToList();

            if (availableSeats.Count < amount)
            {
                throw new ArgumentException("Not enough seats available.");
            }


            int totalPrice = 0;

            foreach (var seat in availableSeats)
            {
                seat.IsAvailable = false;

                int seatPrice = seat.Type switch
                {
                    SeatType.BALCONY => 80,
                    SeatType.REGULAR => 51,
                    SeatType.WHEELCHAIR => 30,
                };

                totalPrice += seatPrice;

            }

            Ticket ticket = new Ticket(
                GenerateNewId(),
                $"Ticket für {play.Title}",
                price: totalPrice,
                amount: amount,
                guestName: UserName,
                saleDate: DateTime.Now,
                play: play
                );
            
            play.Tickets.Add(ticket);

            return ticket;
        }

        // Hilfmethode
        private static int GenerateNewId()
        {
            return new Random().Next(1000, 9999);
        }

    }
}