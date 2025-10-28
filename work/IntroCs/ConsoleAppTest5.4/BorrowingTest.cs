using ConsoleApp5._4.Items;
using ConsoleApp5._4;
using ConsoleApp5._4.Users;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp5._4.Enum;
using ConsoleApp5._4.HelperClasses;

namespace ConsoleAppTest5._4
{
    [TestClass]
    public class BorrowingTest
    {
        private User _user;
        private Item _item;

        [TestInitialize]
        public void Setup()
        {
            _user = new Teacher("Schmid");
            _item = new Book("Last Road", "Penguin");
        }


        [TestMethod]
        public void IsObject_OfCorrectType()
        {
            // Arrange
            var now = DateTime.Now;
            var borrowing = new Borrowing(_user, _item, now, now.AddDays(1));

            // Act
            var result = Result<Borrowing>.Ok(borrowing, "Created");

            // Assert
            Assert.IsInstanceOfType(result, typeof(Result<Borrowing>));
            Assert.IsNotNull(result.Data);
            Assert.AreSame(borrowing, result.Data);
            Assert.IsFalse(borrowing.IsReturned);
        }

        [TestMethod]
        public void IsObject_WithCorrectData()
        {

            // Arrange
            var now = DateTime.Now;
            var borrowing = new Borrowing(_user, _item, now, now.AddDays(1));

            // Act
            var result = Result<Borrowing>.Ok(borrowing, "Created");

            // Assert 
            Assert.AreEqual(_user.Extensions, borrowing.User.Extensions);
            Assert.AreEqual(_user.LoanFees, borrowing.User.LoanFees);

        }
    }
}