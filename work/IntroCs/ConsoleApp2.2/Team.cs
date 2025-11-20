namespace ConsoleApp2._2 
{

    public class Team
    {
        public string TeamName { get; set; }
        public List<Player> Players { get; set; }

        public Team(string name, List<Player> players)
        {
            TeamName = name;
            Players = players;
        }
    }
}