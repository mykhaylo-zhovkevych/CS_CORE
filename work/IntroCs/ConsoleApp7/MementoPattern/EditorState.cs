namespace MementoPattern
{
    // Memento 
    public class  EditorState
    {
        // once the editor state has been created, it cannot be changed
        private readonly string _title;
        private readonly string _content;

        // State meta Data
        private readonly DateTime _stateCreatedAt;


        public EditorState(string title, string content)
        {
            _title = title;
            _content = content;
            // Meta Data
            _stateCreatedAt = DateTime.Now;
        }

        public string GetTitle()
        {
            return _title;
        }

        public string GetContent()
        {
            return _content;
        }

        public DateTime GetDate()
        {
            return _stateCreatedAt;
        }

        // Unique identifier
        public string GetName()
        {
            return $"{_stateCreatedAt}/ {_title}";
        }

    }
}