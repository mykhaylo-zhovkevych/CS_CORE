namespace MementoPattern
{

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
            _stateCreatedAt = DateTime.Now;
        }
        // 2.18.48

        public string GetTitle()
        {
            return _title;
        }

        public string GetContent()
        {
            return _content;
        }

    }

}