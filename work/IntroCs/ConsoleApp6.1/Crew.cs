using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6._1
{
    public class Crew
    {

        public ArrayList members { get; private set; }


        public Crew()
        {
            members = new ArrayList();
            members.Add(new CrewMember("Alice", Roles.LineCook));
            members.Add(new CrewMember("Bob", Roles.Chef));
            //members.Add(new CrewMember("Bob", Roles.Server));
            //members.Add(new CrewMember("Bob", Roles.Host));
            //members.Add(new CrewMember("Bob", Roles.SousChef));
            //members.Add(new CrewMember("Bob", Roles.Dishwasher));

        }
        

        class CrewMember
        {
            public string Name { get; private set; }
            public Enum Role { get; private set; }
            public CrewMember(string name, Enum role)
            {
                Name = name;
                Role = role;
            }
        }

        public enum Roles
            {
                Chef,
                SousChef,
                LineCook,
                Dishwasher,
                Server,
                Host
            }
        
    }
}
