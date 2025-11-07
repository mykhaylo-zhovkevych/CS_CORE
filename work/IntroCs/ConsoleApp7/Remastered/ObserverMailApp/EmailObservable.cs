namespace ObserverMailApp
{
    // dosent matter which type of observer is there, it must just implement this interface IObserver
    public class EmailObservable : IObserver
    {
        private string _emailTitle;

        public EmailObservable(string emailTitle)
        {
            _emailTitle = emailTitle;
        }

        public void Update(string message)
        {
            Console.WriteLine($"Email '{_emailTitle}' received new message: {message}");
        }
    }
}