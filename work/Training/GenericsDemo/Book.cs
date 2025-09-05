using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GenericsDemo
{

    /*
     IComparable<T> Used to define a default comparison between objects of the same type.
     It allows objects to be sorted automatically 
     Requires me to implement the method: int CompareTo(T other);

     IEnumerable<T> Provides iteration over a collection. The basis of foreach in C#

     IList<> Extends IEnumerable<T> and ICollection<T>
     Represents an ordered collection with indexing (like arrays of lists)
     Provides methods like Add, Remove, Insert, and access via [index]
     */



    public class Book : IComparable<Book>
    {

        public string Title { get; set; }
        public int Pages { get; set; }

        public int CompareTo(Book other)
        {
            if (other == null) return 1;
            return this.Pages.CompareTo(other.Pages);
        }

        public override string ToString() => $"{Title} ({Pages} pages)";
    }

    public class BookCollection : IList<Book>
    {
        private List<Book> _books = new List<Book>();

        // Indexer
        public Book this[int index]
        {
            get => _books[index];
            set => _books[index] = value;
        }
        public int Count => _books.Count;
        public bool IsReadOnly => false;

        public void Add(Book item) => _books.Add(item);
        public void Clear() => _books.Clear();
        public bool Contains(Book item) => _books.Contains(item);
        public void CopyTo(Book[] array, int arrayIndex) => _books.CopyTo(array, arrayIndex);
        public IEnumerator<Book> GetEnumerator() => _books.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int IndexOf(Book item) => _books.IndexOf(item);
        public void Insert(int index, Book item) => _books.Insert(index, item);
        public bool Remove(Book item) => _books.Remove(item);
        public void RemoveAt(int index) => _books.RemoveAt(index);
    }

    class Execuror
    {
        static void Main()
        {
            BookCollection books = new BookCollection
            {
                new Book { Title = "C# in Depth", Pages = 900 },
                new Book { Title = "The Paragamtic Programmer", Pages = 250 },
                new Book { Title = "Clean Code", Pages = 450 }
            };

            Console.WriteLine("Book in collection: ");
            foreach ( var book in books )
            {
                Console.WriteLine($"{book.Title}");
            }

            Console.WriteLine("\nSorted books by pages:");
            List<Book> sorted = new List<Book>(books);
            sorted.Sort();
            foreach (var book in sorted)
                Console.WriteLine(book);


        }
    }
}
