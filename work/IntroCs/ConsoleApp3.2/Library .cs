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

        private List<(User ReservedBy, Object Obj)> _reservations;
        private List<(User BorrowedBy, Object Obj)> _borrowed;


        public Library(string name, string address)
        {
            Name = name;
            Address = address;
            _shelves = new List<Shelf>();
            _reservations = new List<(User, Object)>();
            _borrowed = new List<(User, Object)>();
        }

        internal bool IsObjectAvailable(Object obj)
        {
            if (obj.IsReserved) return false;
            return (obj.ReturnDate == null) || (obj.ReturnDate < DateTime.Today);
        }

        internal void BorrowObject(User user, Object obj)
        {
            var firstReservation = _reservations.FirstOrDefault(r => r.Obj == obj);

            if ( firstReservation.Obj != null && firstReservation.ReservedBy != user)
            {
                Console.WriteLine($"{obj.Name} is reserved for {firstReservation.ReservedBy.Name} + User: {user.Name} cannot borrow it");
            }

            if (IsObjectAvailable(obj))
            {
                _reservations.RemoveAll(r => r.Obj == obj && r.ReservedBy == user);

                int maxDays = GetMaxRentingDays(user);
                obj.ReturnDate = DateTime.Today.AddDays(maxDays);

                double totalPrice = obj.CalcObjectPrice() * user.PriceFactor;
                _borrowed.Add((user, obj));

                Console.WriteLine($"{user.Name} has booked {obj.Name} until {obj.ReturnDate}\nTotal rental price: {totalPrice:F2}");
            }
  
        }

        internal void ExtendRentPeriod(Object obj)
        {
            var borrowedEntry = _borrowed.FirstOrDefault(b => b.Obj == obj);

            if (borrowedEntry.Obj != null || obj.ReturnDate.HasValue)
            {
                if (DateTime.Today > obj.ReturnDate.Value)
                {
                    Console.WriteLine(obj.Name + " is already overdue, cannot extend.");
                    return;
                }
                
                // pseudo test if already extended
                int maxDays = GetMaxRentingDays(borrowedEntry.BorrowedBy);
                DateTime initialReturnDate = DateTime.Today.AddDays(maxDays);

                if (obj.ReturnDate > initialReturnDate)
                {
                    Console.WriteLine($"{borrowedEntry.BorrowedBy.Name} cannot extend again for {obj.Name}");
                    return;
                }

                obj.ReturnDate = obj.ReturnDate.Value.AddDays(maxDays);
                Console.WriteLine($"Return date extended by {maxDays} days, until {obj.ReturnDate.Value.ToShortDateString()}");
            }
            else
            {
                Console.WriteLine("Error: Cannot extend rent period (not borrowed).");
            }
        }

        internal void ReserveObject(User user, Object obj)
        {
            var existingReservation = _reservations.FirstOrDefault(r => r.Obj == obj);
            if (existingReservation.Obj != null)
            {
                Console.WriteLine($"{obj.Name} is already reserved by {existingReservation.ReservedBy.Name}, Sorry {user.Name}");
            }
            obj.IsReserved = true;
            _reservations.Add((user, obj));
            Console.WriteLine($"{user.Name} has reserved {obj.Name}");

        }


        internal void ReturnObject(Object obj)
        {
            var borrowedEntry = _borrowed.FirstOrDefault(b => b.Obj == obj);
            if (borrowedEntry.Obj != null)
                _borrowed.Remove(borrowedEntry);

            var firstReservation = _reservations.FirstOrDefault(r => r.Obj == obj);
            if (firstReservation.Obj != null)
            {
                var nextUser = firstReservation.ReservedBy;
                _reservations.Remove(firstReservation);
                BorrowObject(nextUser, obj);

                Console.WriteLine($"{obj.Name} has been returned.");
            }
            else
            {
                obj.IsReserved = false;
                obj.ReturnDate = null;
                Console.WriteLine($"{obj.Name} is now available");
            }
        }

        internal void PrintBorrowedObjects()
        {
            Console.WriteLine("Borrowed Objects:\n");
            foreach (var entry in _borrowed)
            {
                Console.WriteLine($"- {entry.Obj.Name}, borrowed by {entry.BorrowedBy.Name}, return by {entry.Obj.ReturnDate?.ToShortDateString()}");
            }
        }

        // Hilfsklasse
        internal int GetMaxRentingDays(User user)
        {
            if (user is Teacher)
                return 10;
            else if (user is Student)
                return 8;
            else if (user is ExternalUser)
                return 6;
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