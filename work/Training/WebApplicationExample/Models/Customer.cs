namespace WebApplicationExample.Models
{
    public class Customer
    {
        public string Name { get; set; } = null!;
        public int Id { get; set; }
        public int Age { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<Contact> contacts { get; set; } = new List<Contact>();

        public class Contact
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public string Detail { get; set; }
        }

        public class Address
        {
            public int Id { get; set; }
            public string Street { get; set; } = null!;
            public string City { get; set; } = null!;
        }
    }
}

