using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._1
{
    internal class CellarStorage<T> : IEnumerable<T>
    {

        private Stack<T> items;
        private int capacity;
        private const int DefaultCapacity = 10;


        public CellarStorage(int capacity = DefaultCapacity)
        {
            this.capacity = capacity;
            items = new Stack<T>(capacity);
        }

        public void PushItemToStorage(T value)
        {
            if (items.Count > capacity)
                throw new InvalidOperationException("Cellar is full");
            items.Push(value);
        }

        public void RemoveItemFromStorage()
        {
            if (items.Count == 0)
                throw new InvalidOperationException("Cellar is empty");
            items.Pop();
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }

        public bool IsFull ()
        {
            return items.Count > 0;
        }

        public void PrintAll()
        {
            Console.WriteLine("Current items in cellar: ");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Returns an Enumerator, which allows the use of a foreach loop without needing to implement it manually
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
