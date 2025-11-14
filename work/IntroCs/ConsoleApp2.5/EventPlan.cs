namespace ConsoleApp2._5
{
    public class EventPlan
    {

        public int Id { get; private set; }
        public string Season { get; private set; }
        public List<Play> Plays { get; set; } = new List<Play>();

        public EventPlan(int id, string season)
        {
            Id = id;
            Season = season;
        }

    }
}
