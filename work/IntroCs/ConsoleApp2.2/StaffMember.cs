namespace ConsoleApp2._2
{
    public class StaffMember
    {
       public Referee Referee { get; set; }
       public RefereeRole Role { get; set; }

        public override string ToString()
        {
            return $"{Referee.Name} ({Role})";
        }

    }
}