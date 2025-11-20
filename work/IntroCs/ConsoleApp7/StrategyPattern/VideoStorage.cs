namespace StrategyPattern
{

    public class VideoStorage
    {
        private Compressor _compressor;
        private Overlay _overlay;

        public VideoStorage(Compressor compressor, Overlay overlay = Overlay.None)
        {
            _compressor = compressor;
            _overlay = overlay;
        }


        public void SetCompressor(Compressor Compressor)
        {
            _compressor = Compressor;
        }

        public void SetOverlay(Overlay overlay)
        {
            _overlay = overlay;
        }   

        public void Store(string fileName)
        {
            // Compression logic
            if (_compressor == Compressor.MOV)
            {
                Console.WriteLine("Storing " + fileName + " using MOV compressor");
            }
            else if (_compressor == Compressor.MP4)
            {
                Console.WriteLine("Storing " + fileName + " using MP4 compressor");
            }
            else if (_compressor == Compressor.WEBM)
            {
                Console.WriteLine("Storing " + fileName + " using WEBM compressor");
            }

            // Overlay logic
            if (_overlay == Overlay.BlackAndWhite)
            {
                Console.WriteLine("Applying BlackAndWhite overlay");
            }
            else if (_overlay == Overlay.Blur)
            {
                Console.WriteLine("Applying Captions overlay");
            }
            else if (_overlay == Overlay.None)
            {
                Console.WriteLine("No overlay applied");
            }

            Console.WriteLine("Storing video to " + fileName + "." + _compressor);

        }
    }
}