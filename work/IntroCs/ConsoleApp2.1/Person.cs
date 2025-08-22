using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._1
{
    internal class Person
    {
        private string _name;
        private int _id;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Person(string name, int id)
        {
            this._name = name;
            this._id = id;
        }
    }
}
