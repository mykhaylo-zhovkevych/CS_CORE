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

        public Ticket BuyTicket(string UserName, int amount, Play play)
        {
            var availableSeats = Seats.Where(s => s.IsAvailable).ToList();
           

            if (availableSeats.Count < amount)
            {
                throw new ArgumentException("Not enough seats available.");
            }

            int totalPrice = 0;

            for (int i = 0; amount > i; i++) 
            {

                var seat = availableSeats[i];
                seat.IsAvailable = false;

                switch (seat.Type)
                {
                    case SeatType.REGULAR:
                        totalPrice += 51;
                        break;
                    case SeatType.BALCONY:
                        totalPrice += 80;
                        break;
                    
                    case SeatType.WHEELCHAIR:
                        totalPrice += 30;
                        break;
                }

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