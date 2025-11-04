namespace DecoratorPattern
{
    public class CompressedData : CloudData
    {

        public CompressedData(string url) : base(url)
        {

        }

        public override void Save(string data)
        {
            var compressed = Compress(data);
            base.Save(compressed);
        }

        // Simulation of compressing the Data
        public string Compress(string data)
        {
            return data.Substring(0, 9);
        }

    }
}