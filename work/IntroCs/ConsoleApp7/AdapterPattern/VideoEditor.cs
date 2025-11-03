using System.Drawing;

namespace AdapterPattern
{

    public class VideoEditor
    {
        private Video _video;

        public VideoEditor(Video video)
        {
            _video = video;
        }
        public void ApplyColor(IColor iColor)
        {
            Console.WriteLine("Starting color processing...");
            iColor.Apply(_video);
            Console.WriteLine("Color processing finished.\n");
        }
    }
}