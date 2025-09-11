using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Delegates_AnonymousDemo
{

    public delegate int CompareCustomers(Customer c1, Customer c2);

    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime SignupDate { get; set; }
    }

    public class CustomerSorter
    {
        public void SortCustomers(List<Customer> customers, CompareCustomers compare)
        {
            // very simple bubble sort, with delegates
            for (int i = 0; i < customers.Count; i++)
            {
                for (int j = i + 1; j < customers.Count; j++)
                {
                    if (compare(customers[i], customers[j]) > 0)
                    {
                        // swap
                        var temp = customers[i];
                        customers[i] = customers[i];
                        customers[j] = temp;
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "Alice", Age = 30, SignupDate = DateTime.Now.AddMonths(3) },
                new Customer { Name = "Bob", Age = 25, SignupDate = DateTime.Now.AddDays(23) },
                new Customer { Name = "Charlie", Age = 33, SignupDate = DateTime.Now },

            };


            CustomerSorter sorter = new CustomerSorter();

            // Sort by Age
            // First paramater is customers, secound is lambra
            sorter.SortCustomers(customers, (c1, c2) => c1.Age.CompareTo(c2.Age));
            Console.WriteLine("Sorted by Age: ");
            customers.ForEach(c => Console.WriteLine($"{c.Name}, {c.Age}, {c.SignupDate:d}"));

            // Sort by SignupDate
            sorter.SortCustomers(customers, (c1, c2) => c1.SignupDate.CompareTo(c2.SignupDate));
            Console.WriteLine("\nSorted by Signup Date:");
            customers.ForEach(c => Console.WriteLine($"{c.Name},{c.Age}, {c.SignupDate:d}"));

        }
    }
}
