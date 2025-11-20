using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MementoPattern
{
    // History manages states but changes the editor
    public class History
    {
        private List<EditorState> _states = new List<EditorState>();

        // Composition because reference to the Editor object
        private Editor _editor;

        public History(Editor editor)
        {
            _editor = editor;
        }

        public void Backup()
        {
            _states.Add(_editor.CreateState());
        }

        public void Undo()
        {
            // No states to restore
            if (_states.Count == 0)
            {
                return;
            }
            EditorState lastState = _states.Last();
            _states.Remove(lastState);

            _editor.Restore(lastState);
        }

        public void ShowHistory()
        {
            Console.WriteLine("\nHistory: Here's the list of mementos:");
            foreach (var state in _states) 
            {
                System.Console.WriteLine(state.GetName());
            }
        }

    }
}
