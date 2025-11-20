namespace MementoPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            var editor = new Editor();
            var history = new History(editor);

            history.Backup();
            editor.Title = "Title 1";
            history.Backup();
            editor.Content = "Content 1";
            history.Backup();
            editor.Title = "Title 2";

            System.Console.WriteLine("Title" + editor.Title);
            System.Console.WriteLine("Content" + editor.Content);

            history.Undo();

            System.Console.WriteLine("Title" + editor.Title);
            System.Console.WriteLine("Content" + editor.Content);

            history.ShowHistory();
        }
    }
}
