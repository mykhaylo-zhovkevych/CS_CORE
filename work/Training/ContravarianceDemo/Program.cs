namespace ContravarianceDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITrainer<Animal> generalTrainer = new GeneralTrainer();

            ITrainer<Dog> dogTrianer = generalTrainer;

            Dog rex = new Dog("Rex");
            Dog buddy = new Dog("Buddy");

            dogTrianer.Train(rex);
            dogTrianer.Train(buddy);
        }
    }
}
