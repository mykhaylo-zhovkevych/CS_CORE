namespace DecoratorPattern.Solution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Simulation of user input data 
            // A lot of design patters is based on moving away from inheritance to composition
            var url = "httpS.//google.cloud.com";
            var data = "This is some data. Hello worlds. Facade, facade";
            var compress = true;
            var encrypted = true;

            IData cloudData = new CloudData(url);

            // Adding one conditional for every feature
            // So i decorate the cloud data with encryption decorator
            // Another words i am adding some additioanl functionality by using composition 
            if (encrypted)
            {
                cloudData = new EncryptionDecorator(cloudData);
            }
            if (compress)
            {
                cloudData = new CompressionDecorator(cloudData);
            }

            cloudData.Save(data);
        }
    }
}
