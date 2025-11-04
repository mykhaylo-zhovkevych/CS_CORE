using FactoryMethodPattern.MVCFramework;

namespace FactoryMethodPattern
{
    public class Controller
    {
        public void Render(string fileName, Dictionary<string, object> data)
        {
            var viewEngine = new BladeViewEngine();
            var html = viewEngine.Render(fileName, data);
            Console.WriteLine(html);
        }
    }
}
