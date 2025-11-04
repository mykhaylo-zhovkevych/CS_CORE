namespace StrategyPattern.Solution
{
    public class CompressorWEBM : ICompressor
    {
        public void Compress()
        {
            Console.WriteLine("Compressing using WEBM Compressor");
        }
    }
}