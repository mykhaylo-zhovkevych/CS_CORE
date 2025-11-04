namespace DecoratorPattern
{

    public class EncryptedData : CloudData
    {
        public EncryptedData(string url) : base(url)
        {

        }

        public override void Save(string data)
        {
            var encrypted = Encrypt(data);
            base.Save(data);
        }

        public string Encrypt(string data)
        {
            return "D*g23WE`F*";
        }

    }
}