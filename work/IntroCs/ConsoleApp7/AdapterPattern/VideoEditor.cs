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
        public void ApplyColor(IColor icolor)
        {
            icolor.Apply(_video);
        }
    }
}