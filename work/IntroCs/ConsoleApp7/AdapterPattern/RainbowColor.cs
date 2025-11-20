using AdapterPattern.Package;

namespace AdapterPattern
{
    // This class is a Adapter
    public class RainbowColor : IColor
    {

        // Based on Composition 
        private Rainbow _rainbow;

        public RainbowColor(Rainbow rainbow)
        {
            _rainbow = rainbow;
        }

        public void Apply(Video video)
        {
            _rainbow.Setup();
            _rainbow.Update(video);
        }
    }
}