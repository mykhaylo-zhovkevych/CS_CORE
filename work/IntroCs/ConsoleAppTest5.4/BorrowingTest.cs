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
        private User user;
        private Item item;
        private User user02;
        private Item item02;



        [TestInitialize]
        public void Setup()
        {
            user = new Teacher(Guid.NewGuid(),"Schmid");
            user02 = new Student(Guid.NewGuid(), "John");

            item = new Book(Guid.NewGuid(), "Last Road", "Penguin");
            item02 = new VideoGame(Guid.NewGuid(), "Video Game", GameType.RPG, 20);
        }


        [TestMethod]
        public void IsObject_OfCorrectType()
        {

            // Arrange
            var borrowing = new Borrowing()
            {
                User = user,
                Item = null,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
            };

            // Act
            var values = Result<Borrowing>.Ok(borrowing,"Created");


            // Assert
            Assert.IsInstanceOfType(values, typeof(Borrowing));

        }

        [TestMethod]
        public void IsObject_WithCorrectData()
        {


            // Arrange
            var borrowing = new Borrowing()
            {
                User = user,
                Item = null,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
            };




            // Act
            var values = Result<Borrowing>.Ok(borrowing, "Created");


            // Assert
            Assert.AreSame(2, user.Extensions);
            Assert.AreSame(50.0m, user.LoanFees);
            Assert.AreSame()
            
        }

    }
}
