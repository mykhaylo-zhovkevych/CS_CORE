namespace MementoGameApp 
{
    // SaveGameManager
    // This is resposible of keeping a track of the Memento objects 
    // It doenst understand what is inside of the memento
    public class CareTaker() 
    {
        private Dictionary<string, Memento> _savedGames = new Dictionary<string, Memento>();


        public void Save(string saveName, Memento memento) 
        {
            // _savedGames[saveName] = memento;
            _savedGames.Add(saveName, memento);

            Console.WriteLine(saveName);
        }

        public Memento Load(string saveName) 
        {
            if (_savedGames.ContainsKey(saveName)) 
            {
                Console.WriteLine(saveName);
                return _savedGames[saveName];
            }
            else 
            {
                throw new Exception("Save not found");
            }
        }

    }
}