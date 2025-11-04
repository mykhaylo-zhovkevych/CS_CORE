using FactoryMethodPattern.MVCFramework;

namespace FactoryMethodPattern
{
    // Some another developer creating this class, they using the MVCF backendramework
    public class OrderController : Controller
    {
        public void listorders()
        {
            var orders = new Dictionary<string, object>
            {
                { "Red Socks", "£12"},
                { "Black T-Shirt", "£22"},
                { "Pink Socks", "£2"}
                // 10.42.30
            };
        }
    }
}
