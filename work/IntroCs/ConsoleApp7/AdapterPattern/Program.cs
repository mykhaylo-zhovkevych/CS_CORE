namespace AdapterPattern
{
    public class Program
    {
        static void Main(string[] args)
        {

            var video = new Video();
            var editor = new VideoEditor(video);

            editor.ApplyColor(new BlackAndWhiteColor());
            editor.ApplyColor(new MidnightColor());

            // Applying A external Color thanks to the RainbowCaller that is a Wrapper/Adapter
            editor.ApplyColor(new RainbowColor(new Package.Rainbow()));
        }
    }
}
