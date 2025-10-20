using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAnimalShelter
{
    public class GeneralTrainer : ITrainer<Animal>
    {
        public void Train(Animal a)
        {
            Console.WriteLine($"training {a.GetType().Name}: {a.Name}");
        }
    }
}
