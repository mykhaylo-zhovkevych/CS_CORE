using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._5
{
    internal class Ticket
    {
        public int Id { get; private set; }
        public string TicketName { get; private set; }
        public int Price { get; private set; }
        public int Amount { get; private set; }
        public string GuestName { get; private set; }
        public DateTime SaleDate { get; private set; }
        public Play Play { get; set; }

        public Ticket(int id, string ticketName, int price, int amount, string guestName, DateTime saleDate, Play play)
        {
            Id = id;
            TicketName = ticketName;
            Price = price;
            Amount = amount;
            GuestName = guestName;
            SaleDate = saleDate;
            Play = play;
        }
    }
}
