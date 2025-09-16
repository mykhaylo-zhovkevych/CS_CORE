using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._1
{
    internal class StackStorage<T> : IEnumerable<T>
    {

        private T[] _items;
        private int _count = 0;
        private const int _defaultCapacity = 10;

        public int DefaultCapacity { get { return _defaultCapacity; } }
        public int Capacity { get; private set; }
        // Read-Only Properties
        public bool IsFull => _count == Capacity;
        public bool IsEmpty => _count == 0;

        public StackStorage(int capacity = _defaultCapacity)
        {
            Capacity = capacity;
            _items = new T[capacity];
        }
 
        public void Push(T value)
        {
            if(IsFull)
            {
               
                throw new StackExceptions.StackFullException("Cellar is full");
            }

            _items[_count] = value;
            _count++;
        }

        public T Pop()
        {
            if(IsEmpty)
            {
                throw new StackExceptions.StackEmptyException("Cellar is empty");
            }

            _count--;
            T removed = _items[_count];
            // This keyword handels both referance and values types
            // Not really needed
            _items[_count] = default;

            return removed;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = _count - 1; i >= 0; i--)
            {
                sb.Append(_items[i]).Append('\n');
            }
            return sb.ToString();
        }


        //foreach (var item in _items)
        //{
        //    Console.WriteLine(item);
        //}


        /// <summary>
        /// Returns an Enumerator, which allows the use of a foreach loop without needing to implement it manually
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}