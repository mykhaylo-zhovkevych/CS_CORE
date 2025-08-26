using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._4
{
    internal class Certificate
    {
        public string CourseName { get; set; }
        public User AssignedTo { get; set; }
        public DateTime IssueDate { get; private set; }
        public string CertificateId { get; private set; }

        public Certificate(string courseName, User assignedTo)
        {
            CourseName = courseName;
            AssignedTo = assignedTo;
            IssueDate = DateTime.Now;
            CertificateId = GenerateCertificateId();

        }
        private string GenerateCertificateId()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
        }

    }
}
