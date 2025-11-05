using StategyPattern.solution;

namespace StrategyPattern.Solution
{
	public class VideoStorage
	{
		private ICompressor _compressor;
		private IOverlay _overlay;

		public VideoStorage(ICompressor compressor, IOverlay overlay)
		{
			_compressor = compressor;
			_overlay = overlay;
		}


		public void SetCompressor(ICompressor Compressor)
		{
			_compressor = Compressor;
		}

		public void SetOverlay(IOverlay overlay)
		{
			_overlay = overlay;
		}

		public void Store(string fileName)
		{
			// No need check what Algorithm shoud be applied, becuase it does the classes themself
			_compressor.Compress();
			_overlay.Apply();

			Console.WriteLine("Storing video to " + fileName + "." + _compressor);

		}
	}
}