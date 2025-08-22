using System;
using System.Collections.Generic;

class Program
{
    static List<Person> UsersData = new List<Person>
    {
        new Person { Name = "Alice", Surname = "Jobks", Gender = Gender.Female, Age = 25 },
        new Person { Name = "Bob", Surname = "Schmidt", Gender = Gender.Male, Age = 30 }
    };

    static void Main(string[] args)
    {
        Console.WriteLine("---Welcome To Registry Office Console App---");
        Console.WriteLine("1. Start up");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        switch (choice)
        {
            case 1:
                HandleLoginOrRegistration();
                break;
            default:
                Console.WriteLine("Your input is invalid.");
                break;
        }
    }

    static void HandleLoginOrRegistration()
    {
        bool isLoggedIn = false;

        Console.WriteLine("Please enter first and last name");
        string fullname;
        do
        {
            Console.WriteLine("Enter your full name (first and last):");
            fullname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(fullname))
            {
                Console.WriteLine("Full name cannot be empty. Please try again.");
            }
        } while (string.IsNullOrWhiteSpace(fullname));

        string[] parts = fullname.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string name = parts.Length > 0 ? parts[0] : "";
        string surname = parts.Length > 1 ? parts[1] : "";

        Person? user = UsersData.Find(u =>
            u.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
            u.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase));

        if (user == null)
        {
            Console.WriteLine("User not found, let's register a new user...");

            Console.WriteLine("Which is your gender? (1 = Male, 2 = Female)");
            string genderInput = Console.ReadLine() ?? "";
            Gender gender = genderInput == "2" ? Gender.Female : Gender.Male;

            Console.WriteLine("Are you currently married? Answer: \"yes\" or \"no\"");
            string marriedAnswer = Console.ReadLine()?.Trim().ToLower() ?? "no";

            Person? spouse = null;
            string? maidenName = null;

            Console.WriteLine("Almost there! How old are you, e.x.: 23");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age) || age < 10)
            {
                Console.WriteLine("Please enter a valid age (number):");
            }

            if (marriedAnswer == "yes")
            {
                Console.Write("Enter spouse's first and last name: ");
                string spouseFullName = Console.ReadLine() ?? "";
                string[] spouseParts = spouseFullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string spouseFirst = spouseParts.Length > 0 ? spouseParts[0] : "";
                string spouseLast = spouseParts.Length > 1 ? spouseParts[1] : "";

                spouse = UsersData.Find(u =>
                    u.Name.Equals(spouseFirst, StringComparison.OrdinalIgnoreCase) &&
                    u.Surname.Equals(spouseLast, StringComparison.OrdinalIgnoreCase));

                if (spouse == null)
                {
                    Console.WriteLine($"Spouse '{spouseFirst} {spouseLast}' not found. Creating new record...");
                    spouse = new Person { Name = spouseFirst, Surname = spouseLast };
                    UsersData.Add(spouse);
                }
            }
            else if (gender == Gender.Female)
            {
                Console.Write("What is your maiden name? ");
                maidenName = Console.ReadLine();
            }

            user = new Person
            {
                Name = name,
                Surname = surname,
                Gender = gender,
                Spouse = spouse,
                MaidenName = maidenName,
                Age = age
            };
            UsersData.Add(user);

            isLoggedIn = true;
            Console.WriteLine("Registration complete!");
        }
        else
        {
            isLoggedIn = true;
            Console.WriteLine($"Welcome back {user.Name}!");
        }

        if (isLoggedIn && user != null)
        {
            Console.WriteLine("\n--- Current Users in Registry ---");
            foreach (var u in UsersData)
            {
                Console.WriteLine(u);
            }

            Console.WriteLine("Do you want to get married? yes/no");
            if ((Console.ReadLine() ?? "").Trim().ToLower() == "yes")
            {
                Console.Write("Enter your partner's first and second name: ");
                string partnerName = Console.ReadLine() ?? "";
                var partnerParts = partnerName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string partnerFirst = partnerParts.Length > 0 ? partnerParts[0] : "";
                string partnerLast = partnerParts.Length > 1 ? partnerParts[1] : "";

                Person? partner = UsersData.Find(p =>
                    p.Name.Equals(partnerFirst, StringComparison.OrdinalIgnoreCase) &&
                    p.Surname.Equals(partnerLast, StringComparison.OrdinalIgnoreCase));

                if (partner != null)
                {
                    Wedding.ToMarriage(user, partner);
                }
                else
                {
                    Console.WriteLine("Partner not found in the registry.");
                }
            }
            Console.WriteLine("Do you want to get divorced? yes/no");
            if ((Console.ReadLine() ?? "").Trim().ToLower() == "yes")
            {
                if (!user.IsMarried)
                {
                    Console.WriteLine("You are not married, so you can't get divorced.");
                }
                else
                {
                    Console.WriteLine("Please enter the reason for the divorce:");
                    string reason = Console.ReadLine() ?? "";

                    Wedding.ToDivorce(user, user.Spouse!, reason);
                }
            }
        }
        else
        {
            Console.WriteLine("Login or registration failed. No further actions allowed.");
        }
    }
}

public enum Gender { Male, Female }

public class Person
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    //public DateTime DateOfBirth { get; set; }
    //need take a peek later
    public string? MaidenName { get; set; }
    public Person? Spouse { get; set; }
    public int Age { get; set; }
    public bool IsMarried => Spouse != null;

    public override string ToString()
    {
        string spouseName = Spouse != null ? $"{Spouse.Name} {Spouse.Surname}" : "None";
        return $"{Name} {Surname}, Age: {Age}, Gender: {Gender}, Spouse: {spouseName}";
    }
}

public class Wedding
{
    public static bool ToMarriage(Person p1, Person p2)
    {
        if (p1.IsMarried || p2.IsMarried)
        {
            Console.WriteLine("One of the persons is already married.");
            return false;
        }
        if (p1.Age < 18 || p2.Age < 18)
        {
            Console.WriteLine("One of the persons is under 18.");
            return false;
        }
        p1.Spouse = p2;
        p2.Spouse = p1;

        Console.WriteLine($"{p1.Name} {p1.Surname} and {p2.Name} {p2.Surname} are now married!");
        return true;
    }
    public static bool ToDivorce(Person p1, Person p2, string reason)
    {
        if (!p1.IsMarried || !p2.IsMarried || p1.Spouse != p2 || p2.Spouse != p1)
        {
            Console.WriteLine("These two are not married to each other.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            Console.WriteLine("Divorce must have a reason.");
            return false;
        }

        Console.WriteLine($"Divorce reason: {reason}");

        if (p1.Gender == Gender.Female && !string.IsNullOrWhiteSpace(p1.MaidenName))
        {
            p1.Surname = p1.MaidenName;
        }
        if (p2.Gender == Gender.Female && !string.IsNullOrWhiteSpace(p2.MaidenName))
        {
            p2.Surname = p2.MaidenName;
        }

        // Break marriage
        p1.Spouse = null;
        p2.Spouse = null;

        Console.WriteLine($"{p1.Name} {p1.Surname} and {p2.Name} {p2.Surname} are now divorced.");
        return true;
    }

}