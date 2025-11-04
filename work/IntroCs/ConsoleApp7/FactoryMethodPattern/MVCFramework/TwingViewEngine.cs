namespace FactoryMethodPattern.MVCFramework
{
    public class TwingViewEngine : IViewEngine
    {
        public string Render(string fileName, Dictionary<string, object> data)
        {
            return "View rendered from " + fileName + " using Twing";
        }
    }
}