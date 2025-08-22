using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._1
{
    internal class Library
    {
        public string Name { get; set; }
        public string Address { get; set; }

        private List<BookShelf> _bookShelves;
        private List<Book> _borrowedBooks;
        private List<Person> _borrowers;

        public Library(string name, string address)
        {
            Name = name;
            Address = address;
            _bookShelves = new List<BookShelf>();
            _borrowedBooks = new List<Book>();
            _borrowers = new List<Person>();
        }

        public void AddBookShelf(BookShelf shelf)
        {
            _bookShelves.Add(shelf);
        }

        
        public List<Book> SearchBook(int isba)
        {
            List<Book> foundBooks = new List<Book>();
            foreach (var shelf in _bookShelves)
            {
                foreach (var book in shelf.GetBooks())
                {
                    if (book.number == isba)
                    {
                        foundBooks.Add(book);
                    }
                }
            }
            return foundBooks;
        }





        public BookShelf? SearchBookByShelf(int shelfNumber)
        {
            foreach (var shelf in _bookShelves)
            {
                if (shelf.ShelfNumber == shelfNumber)
                {
                    return shelf;
                }
            }
            return null; // If no shelf found with the given number
        }

        public List<Book> ListAllBooks()
        {
            List<Book> allBooks = new List<Book>();
            foreach (var shelf in _bookShelves)
            {
                // AddRange is used to add all books from the current shelf to the allBooks list
                allBooks.AddRange(shelf.GetBooks());
            }
            return allBooks;
        }

        public void BorrowBook(Person person, Book book, DateTime returnDate)
        {
            if (IsBookAvailable(book.number))
            {
                foreach (var shelf in _bookShelves)
                {
                    if (shelf.GetBooks().Contains(book))
                    {
                        shelf.RemoveBookFromShelf(book);
                        break;
                    }
                }

                book.returnDate = returnDate;
                _borrowedBooks.Add(book);
                _borrowers.Add(person);
            }
            else
            {
                Console.WriteLine("Book is not available for borrowing.");
            }
        }

        public List<Book> GetAllborrowedBooks()
        {
            return new List<Book>(_borrowedBooks);
        }

        public List<Book> GetAllBorrowedByPerson(Person person)
        {
            List<Book> booksByPerson = new List<Book>();

            for (int i = 0; i < _borrowers.Count; i++)
            {
                if (_borrowers[i].Id == person.Id)
                {
                    booksByPerson.Add(_borrowedBooks[i]);
                }
            }
            return booksByPerson;
        }

        public void ReturnBook(int number, Book book)
        {
            int index = _borrowedBooks.IndexOf(book);

            if (index != -1)
            {
                var shelf = SearchBookByShelf(1);
                if (shelf != null)
                {
                    book.returnDate = null;
                    shelf.AddBookToShelf(book);
                    _borrowedBooks.RemoveAt(index);
                    _borrowers.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("Could not find a bookshelf to return the book to.");
                }
            }
            else
            {
                Console.WriteLine("This book was not borrowed from this library.");
            }
        }

        public bool IsBookAvailable(int number)
        {
      
            foreach (var borrowedBook in _borrowedBooks)
            {
                if (borrowedBook.number == number)
                {
                    return false;
                }
            }

            foreach (var shelf in _bookShelves)
            {
                foreach (var book in shelf.GetBooks())
                {
                    if (book.number == number)
                    {
                        return true;
                    }
                }
            }
            // Book dont exist in the library
            return false;
        }

    }
}
