using FactoryMethodPattern.MVCFramework;

namespace FactoryMethodPattern
{
    public class Controller
    {
        public void Render(string fileName, Dictionary<string, object> data)
        {
            // Developer has no way of chaninging to the different view engine 
            // var viewEngine = new BladeViewEngine(); // No need to create a view engine because it is being passed here
            var viewEngine = CreateViewEngine(); // Factory Method 
            var html = viewEngine.Render(fileName, data);
            Console.WriteLine(html);
        }

        protected virtual IViewEngine CreateViewEngine()
        {
            // default implementation
            return new BladeViewEngine();
        }
    }
}