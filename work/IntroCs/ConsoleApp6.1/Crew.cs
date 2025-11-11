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
        public List<CrewMember> Members { get; private set; }

        public Crew()
        {
            Members = new List<CrewMember>();
            Members.Add(new CrewMember("Steuu",1021, Roles.Chef));
            Members.Add(new CrewMember("Alice",2032, Roles.LineCook));
            Members.Add(new CrewMember("Bob",1022, Roles.Chef));
            //members.Add(new CrewMember("Bob", Roles.Server));
            //members.Add(new CrewMember("Bob", Roles.Host));
            //members.Add(new CrewMember("Bob", Roles.SousChef));
            //members.Add(new CrewMember("Bob", Roles.Dishwasher));
        }

        public class CrewMember
        {
            public string Name { get; private set; }
            public int PersonalId { get; private set; }
            public Roles Role { get; private set; }
            public bool IsBusy { get; set; } = false;

            public CrewMember(string name, int personalId, Roles role)
            {
                Name = name;
                PersonalId = personalId;
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
