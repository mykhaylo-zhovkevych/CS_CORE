using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    internal class EmailService
    {

        public void SendEmail()
        {
            Connect();
            Authenticate();
            System.Console.WriteLine("Sending Email...");
            Disconnect();
        }

        private void Disconnect()
        {
            Console.WriteLine("Some internal logic");
        }

        private void Authenticate()
        {
            Console.WriteLine("Some internal logic");
        }

        private void Connect()
        {
            Console.WriteLine("Connecting to email server...");
        }

    }
}
