namespace Partial_Extension
{

    public static class CustomerListExtensions
    {
        public static IEnumerable<Customer> FIlterByMinimumAge(
            this List<Customer> customers, int minAge)
        {
            return customers
                    .Where(c => c.Age >= minAge)
                    .OrderBy(c => c.LastName)
                    .ThenBy(c => c.FirstName);
        }
    }
}