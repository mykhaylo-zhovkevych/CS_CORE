namespace StrategyPattern.Solution
{
    public class CompressorMOV : ICompressor
    {
        public void Compress()
        {
            Console.WriteLine("Compressing video using MOV compressor");
        }
    }
}