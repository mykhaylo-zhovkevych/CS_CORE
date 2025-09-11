namespace Partial_Extension
{

    public partial class Customer
    {
        private int age;

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age
        {
            get => age;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Age cannot be nagative");
                age = value;
            }
        }

        public Customer(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;

        }
    }
}