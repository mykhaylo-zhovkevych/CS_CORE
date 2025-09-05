using System.Collections.Generic;
using System.Collections;

namespace CollectionsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // static predefined size
            int[] zahlen = new int[3];
            zahlen[0] = 10;
            zahlen[1] = 20;
            zahlen[2] = 30;    

            foreach (int i in zahlen)
            {
                Console.WriteLine(i);
            }

            ArrayList arrayList = new ArrayList();
            arrayList.Add(10);
            arrayList.Add("Text");
            arrayList.Add(3.14);

            // here it is not typesafe
            foreach (var item in arrayList)
            {
                Console.WriteLine(item);
            }

            List<string> namen = new List<string>();
            namen.Add("Anna");
            namen.Add("Tom");
            namen.Add("Lisa");

            foreach (string name in namen)
            {
                Console.WriteLine($"{name}");
            }

            Dictionary<int, string> personen = new Dictionary<int, string>();
            personen.Add(1, "Anna");
            personen.Add(2, "Tom");
            personen.Add(3, "Lisa");

            foreach (var eintrag in personen)
            {
                Console.WriteLine($"ID: {eintrag.Key}, Name: {eintrag.Value}");
            }

            Queue<string> aufgaben = new Queue<string>();
            aufgaben.Enqueue("Task 1");
            aufgaben.Enqueue("Task 2");
            aufgaben.Enqueue("Taks 3");

            while (aufgaben.Count > 0)
            {
                Console.WriteLine("Bearbeitet: " + aufgaben.Dequeue());
            }

            Stack<string> stapel = new Stack<string>();
            stapel.Push("Buch 1");
            stapel.Push("Buch 2");
            stapel.Push("Buch 3");

            while (stapel.Count > 0 )
            {
                Console.WriteLine("Nimmt: " + stapel.Pop());
            }

            SortedList<int, string> stadtListe = new SortedList<int, string>();
            stadtListe.Add(3, "Berlin");
            stadtListe.Add(1, "Hamburg");
            stadtListe.Add(2, "München");

            foreach (var item in stadtListe)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }


            TextEditor editor = new TextEditor();

            editor.Write("Hallo");
            editor.Write("Hallo Welt");
            editor.Write("Hallo Welt!!!");

            editor.Undo(); 
            editor.Undo(); 
            editor.Redo();

        

        }
    }
}
