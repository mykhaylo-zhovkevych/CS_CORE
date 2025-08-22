using System;
using System.Collections.Generic;

namespace RegistryOfficeConsoleApp;

class Refactored
{
    static List<Person> UsersData = new List<Person>
    {
        new Person { Name = "Alice", Surname = "Jobks", Gender = Gender.Female, Age = 25 },
        new Person { Name = "Bob", Surname = "Schmidt", Gender = Gender.Male, Age = 30 }
    };

    static void Main()
    {
        ShowMenu();
    }

    static void ShowMenu()
    {
        Console.WriteLine("--- Welcome To Registry Office ---");
        Console.WriteLine("1. Login / Register");
        Console.WriteLine("2. Exit");

        int choice = PromptInt("Choose an option: ", 1, 2);
        switch (choice)
        {
            case 1: HandleLoginOrRegistration(); break;
            case 2: Console.WriteLine("Goodbye!"); break;
        }
    }

    static void HandleLoginOrRegistration()
    {
        string fullName = PromptNonEmpty("Enter your full name (first and last): ");
        var (firstName, lastName) = SplitName(fullName);

        Person? user = FindUser(firstName, lastName);

        if (user == null)
        {
            Console.WriteLine("User not found, let's register you...");
            user = RegisterUser(firstName, lastName);
            Console.WriteLine("Registration complete!");
        }
        else
        {
            Console.WriteLine($"Welcome back {user.Name}!");
        }

        ShowAllUsers();
        HandleMarriageAndDivorce(user);
    }

    static Person RegisterUser(string name, string surname)
    {
        Gender gender = PromptGender();
        string marriedAnswer = PromptYesNo("Are you married? (yes/no): ");

        Person? spouse = null;
        string? maidenName = null;

        int age = PromptInt("Enter your age: ", 10);

        if (marriedAnswer == "yes")
        {
            spouse = PromptForSpouse();
        }
        else if (gender == Gender.Female)
        {
            maidenName = PromptNonEmpty("Enter your maiden name: ");
        }

        var user = new Person
        {
            Name = name,
            Surname = surname,
            Gender = gender,
            Age = age,
            Spouse = spouse,
            MaidenName = maidenName
        };
        UsersData.Add(user);
        return user;
    }

    static Person? PromptForSpouse()
    {
        var (first, last) = SplitName(PromptNonEmpty("Enter spouse's full name: "));
        Person? spouse = FindUser(first, last);
        if (spouse == null)
        {
            spouse = new Person { Name = first, Surname = last };
            UsersData.Add(spouse);
            Console.WriteLine("Spouse added to registry.");
        }
        return spouse;
    }

    static void HandleMarriageAndDivorce(Person user)
    {
        if (PromptYesNo("Do you want to get married? (yes/no): ") == "yes")
        {
            var partner = PromptForSpouse();
            if (partner != null)
            {
                var (success, message) = Wedding.TryMarriage(user, partner);
                Console.WriteLine(message);
            }
            else Console.WriteLine("Partner not found.");
        }

        if (PromptYesNo("Do you want to get divorced? (yes/no): ") == "yes")
        {
            if (!user.IsMarried)
            {
                Console.WriteLine("You are not married.");
                return;
            }
            string reason = PromptNonEmpty("Reason for divorce: ");
            var (success, message) = Wedding.TryDivorce(user, user.Spouse!, reason);
            Console.WriteLine(message);
        }
    }

    static void ShowAllUsers()
    {
        Console.WriteLine("\n--- Current Registry ---");
        foreach (var u in UsersData)
            Console.WriteLine(u);
    }

    static Person? FindUser(string first, string last) =>
        UsersData.Find(u =>
            u.Name.Equals(first, StringComparison.OrdinalIgnoreCase) &&
            u.Surname.Equals(last, StringComparison.OrdinalIgnoreCase));

    static (string, string) SplitName(string fullName)
    {
        var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return (parts.Length > 0 ? parts[0] : "", parts.Length > 1 ? parts[1] : "");
    }

    // ---- Input helpers ----
    static string PromptNonEmpty(string message)
    {
        string input;
        do
        {
            Console.Write(message);
            input = Console.ReadLine()?.Trim() ?? "";
        } while (string.IsNullOrEmpty(input));
        return input;
    }

    static int PromptInt(string message, int min = int.MinValue, int max = int.MaxValue)
    {
        int value;
        do
        {
            Console.Write(message);
        } while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max);
        return value;
    }

    static string PromptYesNo(string message)
    {
        string input;
        do
        {
            Console.Write(message);
            input = Console.ReadLine()?.Trim().ToLower() ?? "";
        } while (input != "yes" && input != "no");
        return input;
    }

    static Gender PromptGender()
    {
        Console.WriteLine("Select gender: 1 = Male, 2 = Female");
        return PromptInt("Your choice: ", 1, 2) == 2 ? Gender.Female : Gender.Male;
    }
}

public enum Gender { Male, Female }

public class Person
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string? MaidenName { get; set; }
    public Person? Spouse { get; set; }
    public int Age { get; set; }
    public bool IsMarried => Spouse != null;

    public override string ToString()
    {
        string spouseName = IsMarried ? $"{Spouse!.Name} {Spouse.Surname}" : "None";
        return $"{Name} {Surname}, Age: {Age}, Gender: {Gender}, Spouse: {spouseName}";
    }
}

public static class Wedding
{
    public static (bool, string) TryMarriage(Person p1, Person p2)
    {
        if (p1.IsMarried || p2.IsMarried)
            return (false, "One of the persons is already married.");
        if (p1.Age < 18 || p2.Age < 18)
            return (false, "One of the persons is under 18.");

        p1.Spouse = p2;
        p2.Spouse = p1;
        return (true, $"{p1.Name} {p1.Surname} and {p2.Name} {p2.Surname} are now married!");
    }

    public static (bool, string) TryDivorce(Person p1, Person p2, string reason)
    {
        if (!p1.IsMarried || !p2.IsMarried || p1.Spouse != p2)
            return (false, "These two are not married to each other.");
        if (string.IsNullOrWhiteSpace(reason))
            return (false, "Divorce must have a reason.");

        if (p1.Gender == Gender.Female && !string.IsNullOrWhiteSpace(p1.MaidenName))
            p1.Surname = p1.MaidenName;
        if (p2.Gender == Gender.Female && !string.IsNullOrWhiteSpace(p2.MaidenName))
            p2.Surname = p2.MaidenName;

        p1.Spouse = null;
        p2.Spouse = null;
        return (true, $"{p1.Name} {p1.Surname} and {p2.Name} {p2.Surname} are now divorced.");
    }
}