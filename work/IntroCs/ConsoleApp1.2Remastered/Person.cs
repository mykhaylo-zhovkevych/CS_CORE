using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1._2Remastered
{
    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string? MaidenName { get; set; }
        public Person? Spouse { get; set; }
        public int Age { get; set; }
        public bool IsMarried => Spouse != null;

        public override string ToString()
        {
            string spouseName = IsMarried ? $"{Spouse!.Name} {Spouse.Surname}" : "None";
            return $"{Name} {Surname}, Age: {Age}, Gender: {Gender}, Spouse: {spouseName}";
        }
    }
}
