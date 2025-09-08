namespace ConsoleApp1
{
    class Types
    {
    struct ValueTypeExample
    {
        public int Number;
    }
        class ReferenceTypeExample
        {
            public int Number;
        }

        static void Main(string[] args)
        {
            // Value Type Example
            ValueTypeExample val1 = new ValueTypeExample { Number = 10 };
            ValueTypeExample val2 = val1; // Copy of the value
            val2.Number = 20;

            Console.WriteLine($"Value Type - Original: {val1.Number}, Copy: {val2.Number}");

            // Reference Type Example
            ReferenceTypeExample ref1 = new ReferenceTypeExample { Number = 10 };
            ReferenceTypeExample ref2 = ref1; // Copy of the reference
            ref2.Number = 20;

            Console.WriteLine($"Reference Type - Original: {ref1.Number}, Copy: {ref2.Number}");
        }
    }
}

// Value Type - Original: 10, Copy: 20
// Reference Type - Original: 20, Copy: 20