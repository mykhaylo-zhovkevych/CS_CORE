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
        private Item _item02;



        [TestInitialize]
        public void Setup()
        {
            _user = new Teacher(Guid.NewGuid(),"Schmid");
            _item = new Book("Last Road", "Penguin");
            _item02 = new VideoGame("Video Game", GameType.RPG, 20);
        }


        [TestMethod]
        public void IsObject_OfCorrectType()
        {

            // Arrange
            var borrowing = new Borrowing()
            {
                User = _user,
                Item = _item,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
            };

            // Act
            var values = Result<Borrowing>.Ok(borrowing,"Created");


            // Assert
            Assert.IsInstanceOfType(values, typeof(Result<Borrowing>));
            Assert.IsNotNull(_user);
            Assert.IsNotNull(_item);

        }

        [TestMethod]
        public void IsObject_WithCorrectData()
        {


            // Arrange
            var borrowing = new Borrowing()
            {
                User = _user,
                Item = _item,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
            };


            // Act
            var values = Result<Borrowing>.Ok(borrowing, "Created");


            // Assert
            Assert.AreEqual(_user.Extensions, borrowing.User.Extensions);
            Assert.AreEqual(_user.LoanFees, borrowing.User.LoanFees);
            Assert.AreNotEqual(_item02.Name, borrowing.Item.Name);
        }
    }
}