using ConsoleApp3._2.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._2
{
    internal class Shelf
    {
        public int ShelfId { get; private set; }
        private List<Object> _objects;


        public Shelf(int shelfNumber)
        {
            this.ShelfId = shelfNumber;
            this._objects = new List<Object>();
        }

        public void AddObjectToShelf(Object obj)
        {
            _objects.Add(obj);
        }

        public void RemoveObjectFromShelf(Object obj)
        {
            _objects.Remove(obj);
        }
    }
}