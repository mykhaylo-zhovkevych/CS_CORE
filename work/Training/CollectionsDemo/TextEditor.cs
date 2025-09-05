using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;

namespace CollectionsDemo
{
	class TextEditor
	{

		private string text = "";
		private Stack<string> undoStack = new Stack<string>();
		private Stack<string> redoStack = new Stack<string>();

		public void Write(string newText) 
		{

			undoStack.Push(text);

			text = newText;

			redoStack.Push(text);

			Console.WriteLine($"New text: {text}");


		}

		public void Undo()
		{
			if (undoStack.Count > 0)
			{
				redoStack.Push(text);


				text = undoStack.Pop();

				Console.WriteLine($"Undo: {text}");
			}
			else
			{
				Console.WriteLine("Nothing to return");
			}
		}

		public void Redo()
		{

			if (redoStack.Count > 0)
			{
				undoStack.Push(text);


				text = redoStack.Pop();

				Console.WriteLine($"Redo: {text}");
			}
			else
			{
				Console.WriteLine("Nothing to restore");
			}

		}
	}
}