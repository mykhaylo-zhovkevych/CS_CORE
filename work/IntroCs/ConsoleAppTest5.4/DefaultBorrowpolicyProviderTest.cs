using ConsoleApp5._4;
using ConsoleApp5._4.Enum;
using ConsoleApp5._4.Exceptions;
using ConsoleApp5._4.Interface;
using ConsoleApp5._4.Items;
using ConsoleApp5._4.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest5._4
{
    //Helper class
    public class RareBook : Item
    {
        public string Publisher { get; set; }

        public RareBook(Guid id, string name, string publisher) : base (name)
        {
            Publisher = publisher;
        }
    }

    [TestClass]
    public class DefaultBorrowpolicyProviderTest
    {
        private DefaultBorrowPolicyProvider _provider;

        private Teacher _teacher;
        private Student _student;

        private Book _book;
        private RareBook _rareBook;
  
        [TestInitialize]
        public void Setup()
        {
            _provider = new DefaultBorrowPolicyProvider();

            _teacher = new Teacher("TestTeacher");
            _student = new Student("TestStudent");
    
            _book = new Book("TestBook", "TestPublisher");
            _rareBook = new RareBook(Guid.NewGuid(), "RareBook", "RarePublisher");

        }

        [TestMethod]
        public void GetPolicy_WithCorrectData()
        {
            // Arrange
            BorrowPolicy policy;

            // Act
            policy = _provider.GetPolicy(_teacher, _book);

            // Assert
            Assert.AreEqual(30, policy.LoanPeriod);
        }

        [TestMethod]
        public void GetPolicy_WithWrongData_MustThrowAException()
        {

            // Act & Assert
            Assert.ThrowsExactly<NonExistingPolicyException>(() => _provider.GetPolicy(_student, _rareBook));
            //Assert.ThrowsException<NonExistingPolicyException>(() => _provider.GetPolicy(_student, _rareBook));
        }

        [TestMethod]
        public void AddRule_WithCorrectTypes()
        {
            // Arrange 
            BorrowPolicy policy;

            // Act
            _provider.AddRule(typeof(Teacher), typeof(RareBook), new BorrowPolicy(21));
            policy = _provider.GetPolicy(_teacher, _rareBook);


            // Assert
            Assert.AreEqual(21, policy.LoanPeriod);
            
        }

        [TestMethod]
        public void AddRule_WithMissingTypes_MustThrowAException()
        {

            // Act & Assert
            Assert.ThrowsExactly<ArgumentNullException>(() => _provider.AddRule(null, typeof(Book), new BorrowPolicy(90)));

        }
    }
}
