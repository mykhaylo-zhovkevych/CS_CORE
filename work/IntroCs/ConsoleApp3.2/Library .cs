using ConsoleApp3._2.Objects;
using ConsoleApp3._2.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3._2
{
    public class Library
    {
        public string Name { get; set; }
        public string Address { get; set; }

        private List<Shelf> _shelves;
        private List<User> _users;

        public Library(string name, string address)
        {
            Name = name;
            Address = address;
            _shelves = new List<Shelf>();
            _users = new List<User>();
        }
        

        internal bool IsObjectAvailable(Object obj)
        {
            // If ReturnDate is null and is not reserved, or if the return date is in the past
            return (obj.ReturnDate == null && obj.IsReserved == false) || (obj.ReturnDate < DateTime.Today);
        }

        internal void BorrowObject(User user, Object obj)
        {
            if (IsObjectAvailable(obj))
            {
                // I need some logic that looks which type of user it is and modifies the return date
                // teacher get 1 month
                // student get 2 weeks
                // other users get 1 week
                int maxDays = GetMaxRentingDays(user);
                DateTime maxRentDate = DateTime.Today.AddDays(maxDays);
                obj.ReturnDate = maxRentDate;

                
                double totalPrice = obj.CalcObjectPrice();
                if (user is Teacher)
                    totalPrice *= 1.3;
                else if (user is Student)
                    totalPrice *= 0.8;
                else if (user is ExternalUser)
                    totalPrice *= 1.1;

                Console.WriteLine($"{user.Name} has booked '{obj.Name}' untill {maxRentDate.ToShortDateString()}");
                Console.WriteLine($"Total rental price: {totalPrice:F}");
            }
        }

        internal DateTime? ExtendRentPeriod(Object obj)
        {
            if (obj.ReturnDate.HasValue)
            {
                DateTime maxRentDate = obj.ReturnDate.Value.AddDays(30);
                obj.ReturnDate = maxRentDate;
                Console.WriteLine($"Return Date has been extended, until: {obj.ReturnDate.Value.ToShortDateString()}");
                return maxRentDate;
            }
            else
            {
                Console.WriteLine("Error");
                return null;
            }
        }

        internal void ReserveObject(User user, Object obj)
        {
            if (!IsObjectAvailable(obj) && !obj.IsReserved)
            {
                obj.IsReserved = true;
                Console.WriteLine($"{user.Name} has '{obj.Name}' reserved.");
            }
            else if (IsObjectAvailable(obj))
            {
                Console.WriteLine($"Reservation not necessary '{obj.Name}' is available.");
            }
            else
            {
                Console.WriteLine($"Reservation not possible '{obj.Name}'.");
            }
        }


        internal void ReturnObject(Object obj)
        {
            obj.ReturnDate = null;
            if (obj.IsReserved == true)
            {
                // 
            }
        }

        // Hilfsklasse
        internal int GetMaxRentingDays(User user)
        {
            if (user is Teacher)
                return 60;
            else if (user is Student)
                return 14;
            else if (user is ExternalUser)
                return 7;
            else
                return 0;
        }

        // Hilfsmethode
        internal void AddShelf(Shelf shelf)
        {
            _shelves.Add(shelf);
        }
    }
}