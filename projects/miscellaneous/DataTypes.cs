using System;
using System.Collections.Generic; // Für Listen, Dictionaries, Stacks, Queues, HashSets

namespace DataTypesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ganzzahlen
            int number = 42;
            long bigNumber = 9000000000L;
            short smallNumber = 32000;
            byte byteValue = 255;
            sbyte signedByte = -128;

            // Gleitkommazahlen
            float pi = 3.14f;
            double e = 2.71828;
            decimal price = 19.99m;

            // Zeichen & boolesche Werte
            char initial = 'A';
            bool isAlive = true;

            // Zeichenketten
            string message = "Hallo, Welt!";

            // Nullable Typen
            int? nullableInt = null;

            // Arrays
            int[] numbers = { 1, 2, 3 };

            // Enum
            Colors favoriteColor = Colors.Blue;

            // Tupel
            var person = (Name: "Max", Age: 30);

            // Dynamisch typisierte Variable
            dynamic dyn = 42;
            dyn = "Hallo";

            // List - Dynamisches Array
            List<string> fruits = new List<string> { "Apfel", "Banane", "Kirsche" };
            fruits.Add("Mango");

            // Dictionary - Schlüssel-Wert-Paar
            Dictionary<string, int> ageMap = new Dictionary<string, int> {
                { "Max", 30 },
                { "Anna", 25 }
            };
            ageMap["Tom"] = 40;

            // Stack - LIFO (Last In, First Out)
            Stack<int> stack = new Stack<int>();
            stack.Push(10);
            stack.Push(20);

            // Queue - FIFO (First In, First Out)
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("Erster");
            queue.Enqueue("Zweiter");

            // LinkedList - Doppelt verkettete Liste
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddLast(1);
            linkedList.AddLast(2);

            // HashSet - Ungeordnete, einzigartige Elemente
            HashSet<string> uniqueNames = new HashSet<string> { "Max", "Anna", "Tom" };
            uniqueNames.Add("Tom"); // Keine Duplikate erlaubt

            // SortedSet - Sortierte, einzigartige Elemente
            SortedSet<int> sortedNumbers = new SortedSet<int> { 5, 2, 8, 1 };

            // Queue<T> - Prioritätswarteschlange (Queue mit Priorität)
            PriorityQueue<string, int> priorityQueue = new PriorityQueue<string, int>();
            priorityQueue.Enqueue("Dringend", 1);
            priorityQueue.Enqueue("Normal", 2);

            // SortedList - Geordnete Liste von Schlüssel-Wert-Paaren
            SortedList<string, int> sortedList = new SortedList<string, int>
            {
                {"Apfel", 3},
                {"Banane", 5},
                {"Kirsche", 2}
            };

            // SortedDictionary - Geordnetes Dictionary
            SortedDictionary<string, int> sortedDictionary = new SortedDictionary<string, int>
            {
                {"Max", 30},
                {"Anna", 25},
                {"Tom", 40}
            };

            // Record - Immutable Datentyp
            var book = new Book("C# Lernen", "Autor A");

            // Struct - Werttyp mit benutzerdefinierter Struktur
            Point point = new Point(10, 20);

            // KeyValuePair - Paar aus Schlüssel und Wert
            KeyValuePair<string, int> kvp = new KeyValuePair<string, int>("Schlüssel", 123);

            // Tuple-Klasse - Mehrere Werte als Objekt
            Tuple<int, string, bool> tupleExample = new Tuple<int, string, bool>(1, "Hallo", true);

            // Span<T> - Speicherbereich zur optimierten Verarbeitung
            Span<int> spanArray = new int[] { 10, 20, 30 };

            // Ausgabe
            Console.WriteLine($"Zahl: {number}, Pi: {pi}, Farbe: {favoriteColor}");
            Console.WriteLine($"Person: {person.Name}, Alter: {person.Age}");

            Console.WriteLine("Liste der Früchte:");
            foreach (var fruit in fruits) Console.WriteLine(fruit);

            Console.WriteLine("Alterszuordnung:");
            foreach (var entry in ageMap) Console.WriteLine($"{entry.Key}: {entry.Value}");

            Console.WriteLine($"Erstes Element im Stack: {stack.Pop()}");
            Console.WriteLine($"Erstes Element in der Queue: {queue.Dequeue()}");

            Console.WriteLine($"Punkt: ({point.X}, {point.Y})");
            Console.WriteLine($"Buch: {book.Title} von {book.Author}");

            Console.WriteLine("Sortierte Zahlen:");
            foreach (var num in sortedNumbers) Console.WriteLine(num);

            Console.WriteLine($"KeyValuePair: {kvp.Key} - {kvp.Value}");
            Console.WriteLine($"Tuple: {tupleExample.Item1}, {tupleExample.Item2}, {tupleExample.Item3}");

            Console.WriteLine("Span-Inhalte:");
            foreach (var value in spanArray) Console.WriteLine(value);

            Console.WriteLine($"Erstes Element aus der Prioritätswarteschlange: {priorityQueue.Dequeue()}");

            Console.WriteLine("SortedList-Inhalte:");
            foreach (var item in sortedList) Console.WriteLine($"{item.Key}: {item.Value}");

            Console.WriteLine("SortedDictionary-Inhalte:");
            foreach (var item in sortedDictionary) Console.WriteLine($"{item.Key}: {item.Value}");
        }

        // Enum - Aufzählung
        enum Colors { Red, Green, Blue }

        // Record - Unveränderliche Datenstruktur
        public record Book(string Title, string Author);

        // Struct - Werttyp mit benutzerdefinierter Struktur
        public struct Point
        {
            public int X { get; }
            public int Y { get; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
