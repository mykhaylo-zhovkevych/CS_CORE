namespace IteratorPattern
{
    public class ShoppingList
    {
        private List<string> _list = new List<string>();

        public void Push(string itemName)
                    {
            _list.Add(itemName);
        }

        public string Pop()
        {
            var last = _list.Last();
            _list.Remove(last);
            return last;
        }


        public List<string> GetList()
        {
            return _list;
        }

        public Iiterator<string> CreateIterator()
        {
            // Passing the current instance of the ShoppingList to the Iterator
            return new ListIterator(this);
        }

        // Concrete Iterator
        private class ListIterator : Iiterator<string>
        {

            private ShoppingList _shoppingList;
            private int _index;


            public ListIterator(ShoppingList shoppingList)
            {
                _shoppingList = shoppingList;
            }

            public string Current()
            {
                return _shoppingList._list[_index];
            }

            public bool HasNext()
            {
                // Here is the acces to the internal methods such as .Count() is possible becuase the structue is known
                return _index < _shoppingList._list.Count;
            }

            public void Next()
            {
                _index++;
            }
        }

    }
}
