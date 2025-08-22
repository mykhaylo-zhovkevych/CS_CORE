using System;

class Program
{
    static void Main(string[] args)
    {
        Person p = new Person("John", "Smith", 30);
        p.PrintInfo();

        p.ChangeAge();
        p.PrintInfo();

        // Part 02

        Fractions f1 = new Fractions(2, 10);
        Fractions f2 = new Fractions(3, 4);

        Fractions sum = f1.Add(f2);
        Console.WriteLine($"{f1.ToString()} + {f2} = {sum}");

        Fractions exp = f1.Expand(5);
        Console.WriteLine($"{f1} expend 5/5 = {exp}");

        Fractions reduced = f1.Reduce();
        Console.WriteLine($"{f1} reduced = {reduced}");

    }
}

// By default without modifier is determined as private, has no default like in Java
public class Person
{

    public string Name;
    public string Surname;
    public int Age;


    public Person(string name, string surname, int age)
    {
        Name = name;
        Surname = surname;
        Age = age;
    }

    public void ChangeAge()
    {
        Age += 1;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"{Name} {Surname}, {Age} years");
    }
}

// Part 02
public class Fractions
{
    public int Numerator { get; private set; }
    public int Denominator { get; private set; }

    // Constructor
    public Fractions(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }

        Numerator = numerator;
        Denominator = denominator;

    }

    public override string ToString()
    {
        return $"{Numerator}/{Denominator}";
    }

    // e.x. Fraction sum = f1.Add(f2); 
    public Fractions Add(Fractions other)
    {
        /*
        By default I need make the fractions eponymous. This means I need multiply the denominator with the other denominator 
        and than multiply the numerator with the other denominator and vice versa.
        Then I can add the numerators and keep the denominator as it is.
        
        */
        int num = this.Numerator * other.Denominator + other.Numerator * this.Denominator;
        int den = this.Denominator * other.Denominator;
        return new Fractions(num, den);
    }

    public Fractions Subtract(Fractions other)
    {
        int num = this.Numerator * other.Denominator - other.Numerator * this.Denominator;
        int den = this.Denominator * other.Denominator;
        return new Fractions(num, den);
    }

    public Fractions Expand(int factor)
    {
        return new Fractions(this.Numerator * factor, this.Denominator * factor);
    }

    public Fractions Reduce()
    {
        int gcd = GCD(Numerator, Denominator);
        return new Fractions(Numerator / gcd, Denominator / gcd);
    }

    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return Math.Abs(a);
    }
}
