using ConsoleApp2._1;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
 
        Library myLibrary = new Library("City Library", "123 Main St.");

        BookShelf shelf1 = new BookShelf(1);
        BookShelf shelf2 = new BookShelf(2);

        Book book1 = new Book("The C# Guide", 101);
        Book book2 = new Book("UML for Beginners", 102);
        Book book3 = new Book("C", 103);

        myLibrary.AddBookShelf(shelf1);
        myLibrary.AddBookShelf(shelf2);

        shelf1.AddBookToShelf(book1);
        shelf1.AddBookToShelf(book2);
        shelf2.AddBookToShelf(book3);

        Person person1 = new Person("Alice", 1);
        Person person2 = new Person("Bob", 2);


        Console.WriteLine();
        myLibrary.BorrowBook(person1, book1, DateTime.Now.AddDays(14));
        myLibrary.BorrowBook(person1, book3, DateTime.Now);
        Console.WriteLine($"{person1.Name} borrowed {book1.title}");
    

        Console.WriteLine();
        bool isAvailable = myLibrary.IsBookAvailable(1401);
        Console.WriteLine($"Is {book1.title} available? {isAvailable}");

        Console.WriteLine();
        List<Book> borrowed = myLibrary.GetAllborrowedBooks();
        foreach (var book in borrowed)
        {
            Console.WriteLine($"Borrowed: {book.title}");
        }

        Console.WriteLine();
        List<Book> booksByAlice = myLibrary.GetAllBorrowedByPerson(person1);
        foreach (var book in booksByAlice)
        {
            Console.WriteLine($"Book borrowed by {person1.Name}: {book.title}");
        }

        Console.WriteLine();
        myLibrary.ReturnBook(book1.number, book1);
        Console.WriteLine($"{person1.Name} returned {book1.title}.");

        Console.WriteLine();
        isAvailable = myLibrary.IsBookAvailable(101);
        Console.WriteLine($"Is {book1.title} available? {isAvailable}");
    }
}