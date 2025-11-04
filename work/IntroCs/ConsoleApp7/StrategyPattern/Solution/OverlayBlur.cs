using StategyPattern.solution;

namespace StrategyPattern.Solution
{
    public class OverlayBlur : IOverlay
    {
        public void Apply()
        {
            Console.WriteLine("Applying blur filter overlay");
        }
    }
}