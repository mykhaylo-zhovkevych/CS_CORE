using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3._2
{
    internal abstract class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public abstract double PriceFactor { get; }

        public User(Guid id, string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}