using System.Security.Cryptography.X509Certificates;

namespace ExceptionsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
             What are Exceptions

             Exceptions are runtime errors that disrupt the normal flow of a program.
             They are objects derived from the base class System.Exception.
             
             Key points:
             1. Exceptions are used for error handling (instead of return codes).
             2. You "throw" an exception when something goes wrong.
             3. You "catch" exceptions to handle them gracefully.
             

             Advanced Notes:
             

             */

            try
            {
                int x = 10;
                int y = 0;
                int result = x / y;
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Finally block always executes");
            }

            try
            {
                string fileContent = File.ReadAllText("nonexistent.txt");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File not found: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("IO error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong: " + ex.Message);
            }

            try
            {
                ValidateAge(-5);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation failed: {ex.Message}");
            }

            try
            {
                BankAccount account = new BankAccount(100);
                account.Withdraw(200);
            } 
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"Custom exception: {ex.Message}");
            }


            static void ValidateAge(int age)
            {
                if (age < 0)
                    throw new ArgumentException("Age cannot be negative");
            }
        }

        public class InsufficientFundsException : Exception
        {
            // Default message constructor 
            public InsufficientFundsException() : base ("Insufficient funds for this operation.") { }

            // Custom message constructor
            public InsufficientFundsException(string message) : base(message) { }
        }

        public class BankAccount
        {
            public decimal Balance { get; private set; }

            public BankAccount(decimal initialBalance)
            {
                Balance = initialBalance;
            }

            public void Withdraw(decimal amount)
            {
                if (amount > Balance)
                    throw new InsufficientFundsException($"Cannot withdraw {amount}. Balance is only {Balance}.");
                Balance -= amount;
            }
        }
    }
}
