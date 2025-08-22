using System.Data;

namespace ConsoleApp2._2
{
    public class IncidentDecision
    {
        public string Description { get; set; }
        public bool isWrongDecision { get; set; }
        public DateTime DecisionTime { get; set; }

    public override string ToString()
        {
            return $"{Description}, ({isWrongDecision}), {DecisionTime}";
        }
        // return $"{IncidentDecision.Description} ({isWrongDecision})"; 
        //CS0120 An object reference is required for the non-static field, method, or property 'member'

    }
}