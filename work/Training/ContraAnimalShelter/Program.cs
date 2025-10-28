namespace ContraAnimalShelter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITrainer<Animal> general = new GeneralTrainer();

            // Contravarinace allows assigning a trainer for Animal to ITrainer<Dog>
            ITrainer<Dog> dogTrainer = general;
            dogTrainer.Train(new("Rex"));
        }
    }
}
