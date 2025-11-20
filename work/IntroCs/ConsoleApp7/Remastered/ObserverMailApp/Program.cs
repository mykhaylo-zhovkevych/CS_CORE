namespace ObserverMailApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmailPublisher publisher = new EmailPublisher();

            IObserver user01 = new EmailObservable("user01");
            IObserver user02 = new EmailObservable("user02");

            publisher.Attach(user01);
            publisher.Attach(user02);

            publisher.Notify("New Book: Design Patterns in C# ");

        }
    }
}
