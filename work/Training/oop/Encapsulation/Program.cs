// Encapsulation which is fundamental principle of oop and that involves bundling data or method and atributes together in a single unit or class and restricting access to some of the object's components.

using Encapsulation;

//BadbankAccount badAccount = new BadbankAccount();
//badAccount.balance = 1000;

//Console.WriteLine($"Bad Account Balance: {badAccount.balance}");


BankAccount account = new BankAccount(500);

Console.WriteLine($"Account Balance: {account.GetBalance()}");

account.Deposit(200);
Console.WriteLine($"Account Balance after deposit: {account.GetBalance()}");

account.Withdraw(1000);

