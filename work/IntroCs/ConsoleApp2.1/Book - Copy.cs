using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._1
{
    internal class Book
    {
        public string title;
        public int number;
        public DateTime? returnDate;

        public Book(string title, int number)
        {
            this.title = title;
            this.number = number;
            // for new created book
            this.returnDate = null;
        }
    }
}