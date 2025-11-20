namespace ObserverMailApp
{
    // dosent matter which type of observer is there, it must just implement this interface IObserver
    public class EmailObservable : IObserver
    {
        public string EmailTitle { get; private set; }

        public EmailObservable(string emailTitle)
        {
            EmailTitle = emailTitle;
        }

        public void Update(string message)
        {
            Console.WriteLine($"Email '{EmailTitle}' received new message: {message}");
        }
    }
}