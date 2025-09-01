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

        private List<(Object Obj, User ReservedBy)> _reservations;

        public Library(string name, string address)
        {
            Name = name;
            Address = address;
            _shelves = new List<Shelf>();
            _reservations = new List<(Object, User)>();
        }

        internal bool IsObjectAvailable(Object obj)
        {
            if (obj.IsReserved) return false;
            return (obj.ReturnDate == null) || (obj.ReturnDate < DateTime.Today);
        }

        internal string BorrowObject(User user, Object obj)
        {
            var firstReservation = _reservations.FirstOrDefault(r => r.Obj == obj);

            if ( firstReservation.Obj != null && firstReservation.ReservedBy != user)
            {
                return $"{obj.Name} is reserved for {firstReservation.ReservedBy.Name} + Another {user.Name} cannot borrow it";
            }

            if (IsObjectAvailable(obj))
            {
                _reservations.RemoveAll(r => r.Obj == obj && r.ReservedBy == user);

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

                return $"{user.Name} has booked {obj.Name} until {maxRentDate.ToShortDateString()}\nTotal rental price: {totalPrice:F2}";
            }
            return $"{obj.Name} is not available for {user.Name}";
        }

        internal string ExtendRentPeriod(Object obj)
        {
            if (obj.ReturnDate.HasValue)
            {
                // between variable is not necessary, but it makes the code more readable
                obj.ReturnDate = obj.ReturnDate.Value.AddDays(30);

                return $"Return Date has been extended, until: {obj.ReturnDate.Value.ToShortDateString()}";
            }
            else
            {
                return "Error: Cannot extend rent period";
            }
        }

        internal string ReserveObject(User user, Object obj)
        {
            var existingReservation = _reservations.FirstOrDefault(r => r.Obj == obj);
            if (existingReservation.Obj != null)
            {
                return $"{obj.Name} is already reserved by {existingReservation.ReservedBy.Name}, Sorry {user.Name}";
            }
            obj.IsReserved = true;
            _reservations.Add((obj, user));
            return $"{user.Name} has reserved {obj.Name}";

        }


        internal string ReturnObject(Object obj)
        {
            var firstReservation = _reservations.FirstOrDefault(r => r.Obj == obj);

            if (firstReservation.Obj != null)
            {
                var nextUser = firstReservation.ReservedBy;

                _reservations.Remove(firstReservation);
                string borrowMessage= BorrowObject(nextUser, obj);

                return $"{obj.Name} has been returned. {borrowMessage}";
            }
            else
            {
                obj.IsReserved = false;
                obj.ReturnDate = null;
                return $"{obj.Name} is now available";
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