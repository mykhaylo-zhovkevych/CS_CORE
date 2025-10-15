using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.Data
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

    }
}
