using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1._2Remastered
{
    public static class Wedding
    {
        public static (bool, string) TryMarriage(Person p1, Person p2)
        {
            if (p1.IsMarried || p2.IsMarried)
                return (false, "One of the persons is already married.");
            if (p1.Age < 18 || p2.Age < 18)
                return (false, "One of the persons is under 18.");

            p1.Spouse = p2;
            p2.Spouse = p1;
            return (true, $"{p1.Name} {p1.Surname} and {p2.Name} {p2.Surname} are now married!");
        }

        public static (bool, string) TryDivorce(Person p1, Person p2, string reason)
        {
            if (!p1.IsMarried || !p2.IsMarried || p1.Spouse != p2)
                return (false, "These two are not married to each other.");
            if (string.IsNullOrWhiteSpace(reason))
                return (false, "Divorce must have a reason.");

            if (p1.Gender == Gender.Female && !string.IsNullOrWhiteSpace(p1.MaidenName))
                p1.Surname = p1.MaidenName;
            if (p2.Gender == Gender.Female && !string.IsNullOrWhiteSpace(p2.MaidenName))
                p2.Surname = p2.MaidenName;

            p1.Spouse = null;
            p2.Spouse = null;
            return (true, $"{p1.Name} {p1.Surname} and {p2.Name} {p2.Surname} are now divorced.");
        }
    }
}
