using ConsoleApp4._3.Interfaces;
using ConsoleApp4._3.OutputServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._3
{
    public abstract class Field
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public abstract char Symbol { get; }
        public List<Item> Items { get; } = new List<Item>();
        public virtual bool CanEnter { get; set; }
        protected readonly IOutputService outputService;

        public Field(string name, IOutputService output = null) 
        { 
            Id = Guid.NewGuid(); 
            Name = name;
            outputService = output ?? new StringBuilderOutputService();
        }
        public virtual bool MovePlayerToField(Player player) { return CanEnter; }

    }
}