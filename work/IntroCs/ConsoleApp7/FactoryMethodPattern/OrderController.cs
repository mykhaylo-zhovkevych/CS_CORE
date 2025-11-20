using FactoryMethodPattern.MVCFramework;

namespace FactoryMethodPattern
{
    // Some another developer creating this class, they using the MVCF backendramework
    public class OrderController : TwigController
    {
        public void ListOrders()
        {
            var orders = new Dictionary<string, object>
            {
                { "Red Socks", "£12"},
                { "Black T-Shirt", "£22"},
                { "Pink Socks", "£2"}
            };
            Render("order.blade.php", orders);
        }

        public void GetOrder(int id)
        {
            // Simulate getting single order by id from db
            var order = new Dictionary<string, object>
            {
                { "Red Socks", "£12" }
            };

            Render("order.php", order);

        }
    }
}
