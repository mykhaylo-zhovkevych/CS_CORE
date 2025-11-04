using StategyPattern.solution;

namespace StrategyPattern.Solution
{
	public class OverlayBlackAndWhite : IOverlay
	{
		public void Apply()
		{
			Console.WriteLine("Applying black and white overlay");
		}
	}
}