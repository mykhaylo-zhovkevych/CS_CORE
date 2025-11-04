namespace StrategyPattern.Solution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var videoStorage = new VideoStorage(new CompressorMOV(), new OverlayBlackAndWhite());

            videoStorage.Store("/videos/some-movie");

            videoStorage.SetCompressor(new CompressorMP4());
            videoStorage.SetOverlay(new OverlayNone());
            videoStorage.Store("/videos/some-movie");
        }
    }
}
