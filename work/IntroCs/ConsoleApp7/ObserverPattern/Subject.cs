using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    // This Subject class doenst need to know anything about the concrete Observer class
    // It must just knwo that they are some sort of the Observer, so the polymorphism can be applied here
    public class Subject
    {
        private List<Observer> _observers = new List<Observer>();


        public void AddObserver(Observer observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(Observer observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (Observer observer  in _observers)
            {
                observer.Update();
            }
        }

    }
}
