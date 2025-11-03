namespace AdapterPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            var videoEditor = new VideoEditor(new Video());
            // Applying A external Color thanks to the RainbowCaller that is a Wrapper/Adapter
            videoEditor.ApplyColor(new RainbowColor(new Package.Rainbow()));
        }
    }
}
