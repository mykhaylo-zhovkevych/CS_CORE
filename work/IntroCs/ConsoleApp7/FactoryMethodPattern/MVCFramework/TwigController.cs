namespace FactoryMethodPattern.MVCFramework
{
    public class TwigController : Controller
    {
        protected override IViewEngine CreateViewEngine()
        {
            return new TwingViewEngine();
        }
    }
}