using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._1
{
    internal class BookShelf
    {
        private int _shelfNumber;
        private List<Book> _books;

        public int ShelfNumber
        {
            get { return _shelfNumber; }
            set { _shelfNumber = value; }
        }
        public BookShelf(int shelfNumber)
        {
            this._shelfNumber = shelfNumber;
            this._books = new List<Book>();
        }

        public void AddBookToShelf(Book book)
        {
            _books.Add(book);
        }

        public void RemoveBookFromShelf(Book book)
        {
            _books.Remove(book);
        }

        public List<Book> GetBooks()
        {
            return _books;
        }
    }
}
