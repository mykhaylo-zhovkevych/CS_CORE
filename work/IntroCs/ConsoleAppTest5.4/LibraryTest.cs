using ConsoleApp5._4;
using ConsoleApp5._4.Enum;
using ConsoleApp5._4.Exceptions;
using ConsoleApp5._4.HelperClasses;
using ConsoleApp5._4.Items;
using ConsoleApp5._4.Users;
using Microsoft.Extensions.DependencyModel;
using static ConsoleApp5._4.Library;

namespace ConsoleAppTest5._4
{

    public class TestUser : User
    {
        public TestUser(Guid id, string name) : base(id, name)
        {
            LoanFees = 0.0m;
            Extensions = -1;
        }

        public override int LoanPeriod { get; set; }
    }

    [TestClass]
    public sealed class LibraryTest
    {
        private static ConsoleApp5._4.Library _library;
        private static User _student;
        private static User _teacher;
        private static ConsoleApp5._4.Users.Admin _admin;
        private static TestUser _testUser;
        private static Item _book;
        private static Item _videoGame;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            var policyProvider = new DefaultBorrowPolicyProvider();

            _library = new ConsoleApp5._4.Library("Central Library", "123 Main St.", policyProvider);
            var shelf1 = new Shelf(1);
            var shelf2 = new Shelf(2);

            _student = new Student(Guid.NewGuid(), "TestStudentOne");
            _teacher = new Teacher(Guid.NewGuid(), "TestTeacherOne");
            _testUser = new TestUser(Guid.NewGuid(), "TestUserOne");
            _admin = new Admin(Guid.NewGuid(), "TestAdminOne");

            _book = new Book(Guid.NewGuid(), "TestBookOne", "TestPublisherOne");
            _videoGame = new VideoGame(Guid.NewGuid(), "TestVideoGameOne", GameType.RPG, 20);

            shelf1.AddItemToShelf(_book);
            shelf1.AddItemToShelf(_videoGame);

            _library.AddShelf(shelf1);
            _library.AddShelf(shelf2);

        }

        [TestMethod]
        public void BorrowItem_When_DataIsCorrect()
        {
            // Act
            _library.BorrowItem(_student, "TestBookOne");

            // Assert
            Assert.AreEqual(true, _book.IsBorrowed);
        }

        [TestMethod]
        public void BorrowItem_When_DataIsWrong()
        {
            // Arrange 
            var result = _library.BorrowItem(null, "TestBookOne");

            // Assert && Act
            Assert.ThrowsException<ArgumentException>(() => _library.BorrowItem(_teacher, "WrongData"));
            Assert.AreEqual(_book.IsBorrowed, result.Success);
        }

        [TestMethod]
        public void BorrowItem_When_BorrowedIsTrue()
        {
            // Arrange
            _videoGame.IsBorrowed = true;

            var result = _library.BorrowItem(_teacher, _videoGame.Name);

            // Act
            _library.BorrowItem(_teacher, "TestVideoGameOne");

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual($"{_videoGame.Name} is currently not available for borrowing", result.Message);

        }

        [TestMethod]
        public void BorrowItem_When_NonExistingPolicyException()
        {
            // Act & Assert
            Assert.ThrowsException<NonExistingPolicyException>(() => _library.BorrowItem(_testUser, _book.Name));
        }


        [TestMethod]
        public void ReturnItem_When_DataIsCorrect()
        {
            // Act
            _library.BorrowItem(_student, "TestBookOne");

            _library.ReturnItem(_student, "TestBookOne");

            // Assert
            Assert.AreEqual(false, _book.IsBorrowed);
        }


        [TestMethod]
        public void ReturnItem_When_DataIsWrong()
        {
            // Arrange
            var result = _library.ReturnItem(null, _book.Name);

            // Assert & Act
            Assert.IsFalse(result.Success);
            Assert.AreNotEqual(true, _book.IsBorrowed);
            Assert.ThrowsException<ArgumentException>(() => _library.ReturnItem(_teacher, _book.Name));

        }

        //TODO: Find out the way to test it
        [TestMethod] 
        public void ReturnItem_Is_EvenCalled()
        {
            // Arrange 
            var eventRaised = false;

            // Act
            _library.InformReserver += (sender, e) =>
            {
                e.Message = "The event called";
                eventRaised = true;
            };

            _library.BorrowItem(_teacher, "TestVideoGameOne");

            _library.ReturnItem(_teacher, "TestVideoGameOne");

            // Assert
            Assert.IsTrue(eventRaised);
            //Assert.AreEqual("The event called", Item.Message);
        }


        [TestMethod]
        public void ReserveItem_When_DataIsCorrect()
        {
            // Act
            _library.ReserveItem(_student, "TestVideoGameOne");

            // Assert
            Assert.IsTrue(_videoGame.IsReserved);
            Assert.AreEqual(_videoGame.ReservedBy, _student.Id);
        }

        [TestMethod]
        public void ReserveItem_When_DataIsWrong()
        {
            // Arrange 
            var result = _library.ReserveItem(null, _videoGame.Name);

            // Assert & Act
            Assert.ThrowsException<ArgumentException>(() => _library.ReserveItem(_teacher, "WrongData"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ReserveItem_When_ReservedBySomeoneElse()
        {
            // Arrange
            _book.ReservedBy = _testUser.Id;
            var result = _library.ReserveItem(_teacher, _book.Name);

            // Act
            _library.ReserveItem(_teacher, _book.Name);

            // Assert
            Assert.IsTrue(_book.IsReserved);
            Assert.AreEqual($"{_book.Name} is currently not available for reserving", result.Message);

        }

        [TestMethod]
        public void CancleReservation_When_DataIsCorrect()
        {
            // Arrange
            _book.ReservedBy = _teacher.Id;

            // Act
            _library.CancleReservation(_teacher, "TestBookOne");

            // Assert
            Assert.IsFalse(_book.IsReserved);
        }

        [TestMethod]
        public void CancleReservation_When_DataIsWrong()
        {
            // Assert & Act
            Assert.ThrowsException<ArgumentException>(() => _library.CancleReservation(_teacher, "dccsd"));

        }

        [TestMethod]
        public void CancleReservation_For_SomeoneElse()
        {
            // Arrange
            _videoGame.ReservedBy = _teacher.Id;
            var result = _library.CancleReservation(_student, _videoGame.Name);

            // Act
            _library.CancleReservation(_student, _videoGame.Name);

            // Assert
            Assert.IsTrue(_videoGame.IsReserved);
            Assert.AreEqual("You cannot cancel another user's reservation", result.Message);
        }

        [TestMethod]
        public void ExtendBorrowingPeriod_When_DataIsCorrect()
        {
            // Arrange & Act
            _library.BorrowItem(_student, _book.Name);
            var stateBefore = _student.Extensions;
            _library.ExtendBorrowingPeriod(_student, _book.Name);
            var stateAfter = _student.Extensions;

            // Assert
            Assert.IsTrue(stateAfter < stateBefore);
        }

        [TestMethod]
        public void ExtendBorrowingPeriod_When_DataIsWrong()
        {
            // Arrange
            var wrongResult = _library.ExtendBorrowingPeriod(null, _book.Name);

            // Assert & Act
            Assert.IsFalse(wrongResult.Success);
            Assert.ThrowsException<ArgumentException>(() => (_library.ExtendBorrowingPeriod(_teacher, "wefwefwe")));
        }

        [TestMethod]
        public void ExtendBorrowingPeriod_When_IsReservedTrue()
        {
            // Arrange
            _book.ReservedBy = _testUser.Id;

            // Assert & Act
            Assert.ThrowsException<IsAlreadyReservedException>(() => _library.ExtendBorrowingPeriod(_testUser, _book.Name));
        }

        //[TestMethod]
        //public void ExtendBorrowingPeriod_When_NoEnoughExtention()
        //{
  
        //    // Arrange
        //    var extensionsResult = _library.ExtendBorrowingPeriod(_testUser, _videoGame.Name);

        //    // Assert & Act
        //    //_library.BorrowItem(_testUser, _videoGame.Name);
        //    Assert.AreEqual($"{_testUser.Name} don't have enough extentions points", extensionsResult.Message);
        //}


        [TestMethod]
        public void ShowActiveBorrowings_With_ExistingData()
        {
            // Arrange 
            PerformPrintOutput active = _library.ShowActiveBorrowings;

            var sw = new StringWriter();
            Console.SetOut(sw);

            var expectedOutput = $"TestStudentOne has 'TestBookOne' from {DateTime.Now} until {DateTime.Now.AddDays(30)}";

            // Act
            _library.BorrowItem(_student, _book.Name);
            Console.WriteLine(active(_student));

            // Assert
            string actualOutput = sw.ToString().Trim();
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ShowActiveBorrowings_If_DataDontExist()
        {
            // Arrange 
            PerformPrintOutput active = _library.ShowActiveBorrowings;

            var sw = new StringWriter();
            Console.SetOut(sw);

            var expectedOutput = $"{_student.Name} has no active borrowings";

            // Act
            //_library.BorrowItem(_student, _book.Name);
            Console.WriteLine(active(_student));

            // Assert
            string actualOutput = sw.ToString().Trim();
            Assert.AreEqual(expectedOutput, actualOutput);

        }

        [TestMethod]
        public void ShowInactiveBorrowings()
        {
            // Arrange 
            PerformPrintOutput nonActive = _library.ShowInactiveBorrowings;

            var sw = new StringWriter();
            Console.SetOut(sw);

            var expectedOutput = $"TestStudentOne had 'TestBookOne' from {DateTime.Now} until {DateTime.Now.AddDays(30)} that was returned at {(DateTime.Now)}";

            // Act
            _library.BorrowItem(_student, _book.Name);
            _library.ReturnItem(_student, _book.Name);
            Console.WriteLine(nonActive(_student));

            // Assert
            string actualOutput = sw.ToString().Trim();
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ShowInactiveBorrowings_If_DataDontExist()
        {
            // Arrange 
            PerformPrintOutput nonActive = _library.ShowInactiveBorrowings;

            var sw = new StringWriter();
            Console.SetOut(sw);

            var expectedOutput = $"{_student.Name} has no past borrowings";

            // Act
            //_library.BorrowItem(_student, _book.Name);
            Console.WriteLine(nonActive(_student));

            // Assert
            string actualOutput = sw.ToString().Trim();
            Assert.AreEqual(expectedOutput, actualOutput);
        }


        //[TestMethod]
        //public void ShowVideoGameWithSpecificAgeRatingInLibrary()
        //{
        //    // Arrange 
        //    PerformPrintLibraryItemsOutput<string> allVideoGamesInLibrary = _library.ShowVideoGameWithSpecificAgeRatingInLibrary;

        //    var sw = new StringWriter();
        //    Console.SetOut(sw);

        //    var expectedOutput = $"TestVideoGameOne, RPG, 20, Is this game borrowed False";

        //    // Act
        //    Console.WriteLine(allVideoGamesInLibrary(_admin));
        //    string actualOutput = sw.ToString().Trim();

        //    // Assert
        //    Assert.AreEqual(expectedOutput, actualOutput);
        //}

      

        //[TestMethod]
        //public void CountAllBooksInLibrary()
        //{
        //    // Arrange 
        //    // Act
        //    // Assert
        //}

        //[TestMethod]
        //public void ShowAllItemsWithSameName()
        //{
        //    // Arrange 
        //    // Act
        //    // Assert
        //}

    }
}
