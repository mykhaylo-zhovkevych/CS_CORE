using ConsoleApp3._2.Objects;
using ConsoleApp3._2.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3._2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var library = new Library("Central Library", "123 Main St.");
            var shelf1 = new Shelf(1);
            var shelf2 = new Shelf(2);

            var book1 = new Book(1, "The C# Guide", 25.50, 2, 978123456, "Tech Publishers");
            var boardGame1 = new BoardGame(2, "Monopoly", 45.00, 8, 0, GameType.SIMULATOR);
            var magazine1 = new Magazine(3, "Nature Weekly", 5.00, 0, 12, "Nature Inc.");
            library.AddShelf(shelf1);
            library.AddShelf(shelf2);
            shelf1.AddObjectToShelf(book1);
            shelf1.AddObjectToShelf(boardGame1);
            shelf2.AddObjectToShelf(magazine1);

            var student1 = new Student(101, "Alice");
            var teacher1 = new Teacher(102, "Bob");
            var externalUser1 = new ExternalUser(103, "Charlie");
            var externalUser2 = new ExternalUser(104, "Dinna");

            // Testfall1
            Console.WriteLine($"Is {book1.Name} available? {library.IsObjectAvailable(book1)}");

            Console.WriteLine("\nBorrowing 'The C# Guide'...");
            string borrowMessage = library.BorrowObject(student1, book1);
            Console.WriteLine(borrowMessage);

            string extendMessage = library.ExtendRentPeriod(book1);
            Console.WriteLine(extendMessage);

            Console.WriteLine($"Is {book1.Name} available? {library.IsObjectAvailable(book1)}");

            string reserveMessage1 = library.ReserveObject(teacher1, book1);
            Console.WriteLine(reserveMessage1);

            string returnMessage1 = library.ReturnObject(book1);
            Console.WriteLine(returnMessage1);

            string reserveMessage2 = library.ReserveObject(externalUser1, book1);
            Console.WriteLine(reserveMessage2);

            string reserveMessage3 = library.ReserveObject(externalUser2, book1);
            Console.WriteLine(reserveMessage3);

            string returnMessage2 = library.ReturnObject(book1);
            Console.WriteLine(returnMessage2);

            string returnMessage3 = library.ReturnObject(book1);
            Console.WriteLine(returnMessage3);

            string returnMessage4 = library.ReturnObject(book1);
            Console.WriteLine(returnMessage4);

            string returnMessage5 = library.ReturnObject(book1);
            Console.WriteLine(returnMessage5);

            string returnMessage6 = library.ReturnObject(book1);
            Console.WriteLine(returnMessage6);

            string reserveMessage4 = library.ReserveObject(student1, book1);
            Console.WriteLine(reserveMessage4);

            string borrowMessage2 = library.BorrowObject(teacher1, book1);
            Console.WriteLine(borrowMessage2);
        }
    }
}