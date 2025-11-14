using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1._2Remastered
{
    public class RegistryOffice
    {
        static List<Person> UsersData = new List<Person>
        {
            new Person { Name = "Alice", Surname = "Jobks", Gender = Gender.Female, Age = 25 },
            new Person { Name = "Bob", Surname = "Schmidt", Gender = Gender.Male, Age = 30 }
        };

        public static void ShowMenu()
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
}
