using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._5
{
    public class Ticket
    {
        public int Id { get; private set; }
        public string TicketName { get; private set; }
        public int Price { get; private set; }
        public List<int> SeatNumbers { get; private set; } = new List<int>();
        public List<SeatType> SeatTypes { get; private set; } = new List<SeatType>();
        public int Amount => SeatNumbers.Count;
        public string GuestName { get; private set; }
        public DateTime SaleDate { get; private set; }
        public Play Play { get; set; }

        public Ticket(int id, string ticketName, int price, IEnumerable<int> seatNumbers, IEnumerable<SeatType> seatTypes, string guestName, DateTime saleDate, Play play)
        {
            Id = id;
            TicketName = ticketName;
            Price = price;
            SeatNumbers.AddRange(seatNumbers);
            SeatTypes.AddRange(seatTypes);
            GuestName = guestName;
            SaleDate = saleDate;
            Play = play;
        }
    }

}
