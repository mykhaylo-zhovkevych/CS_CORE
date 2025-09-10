namespace EventDemo
{
    public class PetEventArgs : EventArgs
    {
        public string Message { get; }
        public DateTime Time { get; }

        public PetEventArgs(string message)
        {
            Message = message;
            Time = DateTime.Now;
        }
    }

    // Publishers (Sensorens)
    public class FoodSensor
    {
        public event EventHandler<PetEventArgs> FoodLow;

        public void CheckFoodLevel(int foodAmount)
        {
            if (foodAmount < 20)
            {
                OnFoodLow(new PetEventArgs("Food is running low"));
            }
        }
        protected virtual void OnFoodLow(PetEventArgs e)
        {
            FoodLow?.Invoke(this, e);
        }

    }

    public class WaterSensor
    {
        public event EventHandler<PetEventArgs> WaterLow;

        public void CheckWaterLevel(int waterAmount)
        {
            if (waterAmount < 10)
            {
                OnWaterLow(new PetEventArgs("Water is almost empty"));
            }
        }

        protected virtual void OnWaterLow(PetEventArgs e)
        {
            WaterLow?.Invoke(this, e);
        }

    }

    // Subscribers
    public class OwnerNotifier
    {
        public void OnEventReceived(object sender, PetEventArgs e)
        {
            Console.WriteLine($"[Notification to Owner] {e.Message} at {e.Time}");
        }
    }

    public class Logger
    {
        public void OnEventreceived(object sender, PetEventArgs e)
        {
            Console.WriteLine($"[Log] {sender.GetType().Name}: {e.Message}");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var foodSensor = new FoodSensor();
            var waterSensor = new WaterSensor();

            var ownerNotifier = new OwnerNotifier();
            var logger = new Logger();


            foodSensor.FoodLow += ownerNotifier.OnEventReceived;
            foodSensor.FoodLow += logger.OnEventreceived;

            waterSensor.WaterLow += ownerNotifier.OnEventReceived;
            waterSensor.WaterLow += logger.OnEventreceived;

            // Simulation
            foodSensor.CheckFoodLevel(14);
            waterSensor.CheckWaterLevel(5);
        }
    }
}
