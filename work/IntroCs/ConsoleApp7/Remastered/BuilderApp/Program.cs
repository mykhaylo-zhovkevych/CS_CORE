namespace BuilderApp
{
    internal class Program
    {
        static void Main(string[] args)
        {


            SocialMediaPost post = new SocialMediaPostBuilder()
                .AddTitle("The Builder Pattern")
                .AddContent("The Builder Pattern is a creational design pattern that allows for the step-by-step construction of complex objects.")
                .AddAuther("Template Name")
                .AddTag("#Design Patterns")
                .AddTag("#CSharp")
                .AddLinks(new Uri("https://somelink.io"))
                .Build();

            SocialMediaPost post02 = new SocialMediaPostBuilder()
                .AddTitle("The Builder Pattern")
                .AddLinks(new Uri("https://somelink.io"))
                .Build();


            // For quick look I can just debug and see the data if I dan't implement ToString method
            Console.WriteLine(post.ToString());
            Console.WriteLine(post02.ToString());

        }
    }
}
