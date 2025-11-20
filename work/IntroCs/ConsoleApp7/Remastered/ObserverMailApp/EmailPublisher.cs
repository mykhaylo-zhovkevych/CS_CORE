using System;

namespace ObserverMailApp
{
    // Has interested observers(subscribers)
    // Subject
    public class EmailPublisher : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver iobserver)
        {
            _observers.Remove(iobserver);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}