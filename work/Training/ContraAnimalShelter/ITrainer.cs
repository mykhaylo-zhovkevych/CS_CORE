using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAnimalShelter
{
    public interface ITrainer<in T>
    {

        void Train(T animal);

    }
}