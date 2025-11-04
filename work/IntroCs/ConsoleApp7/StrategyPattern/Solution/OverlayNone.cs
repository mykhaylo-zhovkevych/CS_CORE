using StategyPattern.solution;

namespace StrategyPattern.Solution
{
    public class OverlayNone : IOverlay
    {
        public void Apply()
        {
            System.Console.WriteLine("Not applying overlay");
        }
    }
}