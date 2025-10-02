using ConsoleApp3._2.Objects;
using ConsoleApp3._2.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp3._2
{
    internal class Library
    {
        private List<Shelf> _shelves;
        private List<(User ReservedBy, Object Obj)> _reservations;
        private List<(User BorrowedBy, Object Obj)> _borrowed;

        public string Name { get; set; }
        public string Address { get; set; }

        public Library(string name, string address)
        {
            Name = name;
            Address = address;
            _shelves = new List<Shelf>();
            _reservations = new List<(User, Object)>();
            _borrowed = new List<(User, Object)>();
        }

        /// <summary>
        /// Checks whether a given object is currently available for borrowing.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if the object is not borrowed, otherwise false.</returns>
        internal bool IsObjectAvailable(Object obj)
        {
            return !_borrowed.Any(b => b.Obj == obj);
        }

        /// <summary>
        /// Allows a user to borrow an object if it is available and not reserved by another user.
        /// </summary>
        /// <param name="user">The user who wants to borrow the object.</param>
        /// <param name="obj">The object to be borrowed.</param>
        internal void BorrowObject(User user, Object obj)
        {

            var current = _borrowed.FirstOrDefault(b => b.Obj == obj);
            if (current.Obj != null && current.BorrowedBy != user)
            {
                Console.WriteLine($"{obj.Name} is currently borrowed by {current.BorrowedBy.Name} until {obj.ReturnDate?.ToShortDateString()}");
                return;
            }

            var firstReservation = _reservations.FirstOrDefault(r => r.Obj == obj);
            if (firstReservation.Obj != null && firstReservation.ReservedBy != user)
            {
                Console.WriteLine($"{obj.Name} is reserved for {firstReservation.ReservedBy.Name}. {user.Name} cannot borrow it.");
                return; 
            }

            if (IsObjectAvailable(obj))
            {
                _reservations.RemoveAll(r => r.Obj == obj && r.ReservedBy == user);

                int maxDays = GetMaxRentingDays(user);
                obj.ReturnDate = DateTime.Today.AddDays(maxDays);
                obj.BorrowDate = DateTime.Today;

                double totalPrice = obj.CalcObjectPrice() * user.PriceFactor;
                _borrowed.Add((user, obj));

                Console.WriteLine($"{user.Name} has booked {obj.Name} until {obj.ReturnDate}\nTotal rental price: {totalPrice:F2}");
            }
        }

        /// <summary>
        /// Extends the rental period of a borrowed object if allowed by the rules.
        /// </summary>
        /// <param name="obj">The object for which the rental period should be extended.</param>
        internal void ExtendRentPeriod(Object obj)
        {
            var borrowedItem = _borrowed.FirstOrDefault(b => b.Obj == obj);

            if (borrowedItem.Obj == null || !obj.ReturnDate.HasValue && !obj.BorrowDate.HasValue)
            {
                Console.WriteLine("Error: Cannot extend rent period");
                return;
            }

            #pragma warning disable CS8629
            if (DateTime.Today > obj.ReturnDate.Value)
            {
                Console.WriteLine($"{obj.Name} is already overdue, cannot extend.");
                return;
            }


            int maxDays = GetMaxRentingDays(borrowedItem.BorrowedBy);
     
            #pragma warning disable CS8629
            DateTime maxAllowedReturnDate = obj.BorrowDate.Value.AddDays(maxDays * 2);


            if (obj.ReturnDate > maxAllowedReturnDate)
            {
                Console.WriteLine($"{borrowedItem.BorrowedBy.Name} cannot extend again for {obj.Name}");
                return;
            }

            obj.ReturnDate = obj.ReturnDate.Value.AddDays(maxDays);
            Console.WriteLine($"Return date extended by {maxDays} days, until {obj.ReturnDate.Value.ToShortDateString()}");
        }


        /// <summary>
        /// Reserves an object for a specific user if it is not already reserved by another user.
        /// </summary>
        /// <param name="user">The user who wants to reserve the object.</param>
        /// <param name="obj">The object to reserve.</param>
        internal void ReserveObject(User user, Object obj)
        {
  
            DateTime today = DateTime.Today;
            var existingReservation = _reservations.FirstOrDefault(r => r.Obj == obj);

            if (existingReservation.Obj != null )
            {
               if (existingReservation.ReservedBy == user)
               {
                    if (obj.ReservationStart.HasValue && obj.ReservationEnd.HasValue)
                    {

                        if (obj.ReservationExtensionCount < 1)
                        {
                            obj.ReservationEnd = obj.ReservationEnd.Value.AddDays(10);
                            obj.ReservationExtensionCount++;
                            Console.WriteLine($"{user.Name} extends reservation for {obj.Name} until {obj.ReservationEnd.Value.ToShortDateString()}");
                        }
                        else
                        {
                            Console.WriteLine($"{obj.Name} has already been extended once, cannot extend again.");    
                        }
                    }
                }
                else
                {
                   Console.WriteLine($"{obj.Name} is already reserved by {existingReservation.ReservedBy.Name}, Sorry {user.Name}");
                }
            }
            else
            {

                DateTime start = DateTime.Today;
                DateTime end = start.AddDays(10);

                obj.ReservationStart = start;
                obj.ReservationEnd = end;
                obj.IsReserved = true;

                _reservations.Add((user, obj));
                Console.WriteLine($"{user.Name} has reserved {obj.Name}");
            }
        }

        /// <summary>
        /// Returns a borrowed object and assigns it to the next user in the reservation list if available.
        /// </summary>
        /// <param name="obj">The object to return.</param>
        internal void ReturnObject(Object obj)
        {
            var borrowedEntry = _borrowed.FirstOrDefault(b => b.Obj == obj);
            if (borrowedEntry.Obj != null)
                _borrowed.Remove(borrowedEntry);

            var firstReservation = _reservations.FirstOrDefault(r => r.Obj == obj);
            if (firstReservation.Obj != null)
            {
                Console.WriteLine($"{obj.Name} has been returned.");
                var nextUser = firstReservation.ReservedBy;
                _reservations.Remove(firstReservation);
                BorrowObject(nextUser, obj);
            }
            else
            {
                obj.IsReserved = false;
                obj.ReturnDate = null;
                Console.WriteLine($"{obj.Name} is now available");
            }
        }

        /// <summary>
        /// Prints a list of all currently borrowed objects with their borrower and return date.
        /// </summary>
        internal void PrintBorrowedObjects()
        {
            Console.WriteLine("Borrowed Objects:");
            foreach (var item in _borrowed)
            {
                Console.WriteLine($"- {item.Obj.Name}, taken at {item.Obj.BorrowDate?.ToShortDateString()}, borrowed by {item.BorrowedBy.Name}, should be return by {item.Obj.ReturnDate?.ToShortDateString()}");
            }
        }

        /// <summary>
        /// Returns the maximum number of days a given user type can borrow an object.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        internal void AddShelf(Shelf shelf)
        {
            _shelves.Add(shelf);
        }
    }
}