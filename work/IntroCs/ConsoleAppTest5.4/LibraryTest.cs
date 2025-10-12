using ConsoleApp5._4;
using ConsoleApp5._4.Enum;
using ConsoleApp5._4.Exceptions;
using ConsoleApp5._4.HelperClasses;
using ConsoleApp5._4.Interface;
using ConsoleApp5._4.Items;
using ConsoleApp5._4.Users;
using Microsoft.Extensions.DependencyModel;
using System.Data;
using static ConsoleApp5._4.Library;

namespace ConsoleAppTest5._4
{

    public class TestUser : User
    {
        public TestUser(Guid id, string name) : base(id, name)
        {
            // Not Used
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
            var borrowing = _library.BorrowItem(_student, _book.Name);

            // Assert
            Assert.IsNotNull(borrowing);
            Assert.AreEqual(_student, borrowing.Data.User);
            Assert.AreEqual(_book, borrowing.Data.Item);
            Assert.IsTrue(_book.IsBorrowed);
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

        [TestMethod] 
        public void ReturnItem_Is_EvenCalled()
        {
            // Arrange
            _library.BorrowItem(_teacher, _videoGame.Name);

            var borrowed = _library.Borrowings.FirstOrDefault(b => 
                b.User.Id == _teacher.Id && 
                b.Item.Id == _videoGame.Id && 
                b.ReturnDate == null);

            _videoGame.ReservedBy = _student.Id; // ✅ Nur diese Zeile notwendig

            bool eventRaised = false;
            ItemEventArgs? eventArgs = null;

            _library.InformReserver += (s, e) =>
            {
                eventRaised = true;
                eventArgs = e;
            };

            // Act
            _library.ReturnItem(_teacher, _videoGame.Name);

            // Assert
            Assert.IsNotNull(borrowed);
            Assert.IsNotNull(eventArgs);
            Assert.IsTrue(eventRaised);
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
            // Arrange
            _library.BorrowItem(_student, _book.Name);


            var borrowed = _library.Borrowings.FirstOrDefault(b => 
                b.User.Id == _student.Id && 
                b.Item.Id == _book.Id && 
                b.ReturnDate == null);
            

            var oldDue = borrowed.DueDate;
            var oldExtensions = _student.Extensions;

            // Act
            _library.ExtendBorrowingPeriod(_student, _book.Name);

            var updated = _library.Borrowings.FirstOrDefault(b => 
                b.User.Id == _student.Id && 
                b.Item.Id == _book.Id && 
                b.ReturnDate == null);

            // Assert
            Assert.IsNotNull(borrowed);
            Assert.IsNotNull(updated);

            Assert.IsTrue(updated.DueDate > oldDue);
            Assert.AreEqual(oldExtensions - 1, _student.Extensions);
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

        [TestMethod]
        public void ExtendBorrowingPeriod_When_NoEnoughExtention()
        {
            // Arrange
            var policyProvider = new DefaultBorrowPolicyProvider();
            policyProvider.AddRule(typeof(TestUser), typeof(VideoGame), new BorrowPolicy(21));

            var library = new ConsoleApp5._4.Library("TestName", "TestAdress", policyProvider);

            var shelf = new Shelf(1);
            var testVideoGame = new VideoGame(Guid.NewGuid(), "TestVideoGameTwo", GameType.RPG, 20);

            shelf.AddItemToShelf(testVideoGame);
            library.AddShelf(shelf);

            var testUser = new TestUser(Guid.NewGuid(), "TestUserOne")
            {
                Extensions = -1
            };

            // Act
            library.BorrowItem(testUser, testVideoGame.Name);
            var extensionsResult = library.ExtendBorrowingPeriod(testUser, testVideoGame.Name);

            // Assert
            Assert.IsFalse(extensionsResult.Success);
            Assert.AreEqual($"{testUser.Name} don't have enough extentions points", extensionsResult.Message);
        }


        [TestMethod]
        public void ShowActiveBorrowings_With_DataExist()
        {
            // Arrange
            var Now = DateTime.Now;
            var End = Now.AddDays(30);

            var borrowing = new Borrowing
            {
                User = _student,
                Item = _book,
                LoanDate = Now,
                DueDate = End
            };
            _library.Borrowings.Add(borrowing);

            var expectedOutput = $"{_student.Name} has '{_book.Name}' from {Now} until {End}";

            // Act
            var actualOutput = _library.ShowActiveBorrowings(_student);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ShowActiveBorrowings_If_DataDontExist()
        {
            // Arrange
            var expectedOutput = $"{_student.Name} has no active borrowings";

            // Act
            var actualOutput = _library.ShowActiveBorrowings(_student);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }
        [TestMethod]
        public void ShowInactiveBorrowings_With_DataExist()
        {
            // Arrange
            var now = DateTime.Now;
            var end = now.AddDays(30);

            var borrowing = new Borrowing
            {
                User = _student,
                Item = _book,
                LoanDate = now,
                DueDate = end,
                ReturnDate = now.AddHours(1)
            };

            // Act
            _library.Borrowings.Add(borrowing);

            var expectedOutput = $"{_student.Name} had '{_book.Name}' from {now} until {end} that was returned at {borrowing.ReturnDate}";

            var actualOutput = _library.ShowInactiveBorrowings(_student);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ShowInactiveBorrowings_If_DataDontExist()
        {
            // Arrange
            var expectedOutput = $"{_student.Name} has no past borrowings";

            // Act
            var actualOutput = _library.ShowInactiveBorrowings(_student);

            // Assert
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
