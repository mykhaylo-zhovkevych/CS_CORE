using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;
using ConsoleApp5._4Remastered.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace ConsoleAppTest5._4Remastered
{

    [TestClass]
    public class LibraryTest
    {
        private Library _library;

        private Policy _policy;
        private User _teacher;
        private User _student;
        private Item _book;
        private Item _videoGame;


        [TestInitialize]
        public void Setup()
        {

            _library = new Library("Central Library", "123 Main St.");

            _student = new User("TestName", UserType.Student);
            _teacher = new User("TestName02", UserType.Teacher);
            _book = new Item("CS Book", ItemType.Book);
            _videoGame = new Item("TestVideoGameOne", ItemType.VideoGame);

            _policy = new Policy { PolicyName = "Student_BoardGame_Policy", User = _student, Item = _videoGame };
            _policy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriod: 30);
            PolicyService.AddPolicy(_policy);


            _policy = new Policy { PolicyName = "Student_BoardGame_Policy", User = _teacher, Item = _videoGame };

            _policy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriod: 30);

            PolicyService.AddPolicy(_policy);

        }

        [TestMethod]
        public void BorrowItem_When_DataIsCorrect()
        {
            // Act 
            var result = _library.BorrowItem(_student, _book);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(_book.IsBorrowed);
            Assert.AreEqual(1, _library.Borrowings.Count);
        }

        [TestMethod]
        public void BorrowItem_When_DataIsWrong()
        {
            // Arrange 
            var result = _library.BorrowItem(null, _book);

            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(_book.IsBorrowed);

            // Act & Assert
            _book.IsBorrowed = true;
            Assert.ThrowsException<IsAlreadyBorrowedException>(() => _library.BorrowItem(_student, _book));
        }

        [TestMethod]
        public void BorrowItem_When_NoPolicyFound()
        {
            // Arrange
            PolicyService.Policies.Clear();

            // Act
            var result = _library.BorrowItem(_student, _book);

            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(_book.IsBorrowed);
            Assert.AreEqual(0, _library.Borrowings.Count);
        }

        [TestMethod]
        public void TestReturnItem_When_DataExists()
        {
            // Arrange 
            _library.BorrowItem(_student, _book);

            // Act
            var result = _library.ReturnItem(_student, _book);  

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(_book.IsBorrowed);
            var borrowing = _library.Borrowings.First();
            Assert.IsNotNull(borrowing.ReturnDate);

        }

        [TestMethod]
        public void TestReturnItem_When_DataNotFound()
        {
            // Arramge
            _library.BorrowItem(_student, _book);
            var anotherBook = new Item("Another Book", ItemType.Book);

            // Act 
            var result = _library.ReturnItem(_student, anotherBook);

            // Assert
            Assert.IsFalse(result);
            Assert.IsTrue(_book.IsBorrowed);
            Assert.AreSame<Item>(_book, _library.Borrowings.First().Item);
        }


        [TestMethod]
        public void ReserveItem_When_DataIsCorrect()
        {
            // Act
            _library.BorrowItem(_student, _videoGame);
            _library.ReserveItem(_teacher, _videoGame);

            // Assert
            Assert.IsTrue(_videoGame.IsReserved);
            Assert.AreEqual(_videoGame.ReservedBy, _teacher);
        }


        [TestMethod]
        public void ReserveItem_When_DataIsWrong()
        {
            // Arrange 
            var result = _library.ReserveItem(null, _videoGame);

            // Assert & Act
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReserveItem_When_ReservedBySomeoneElse()
        {
            // Arrange
            _book.ReservedBy = _student;

            // Act
            var expected = _book.ReservedBy.Equals(_student);

            _library.ReserveItem(_teacher, _book);

            var result = _book.ReservedBy.Equals(_teacher);

            // Assert
            Assert.IsTrue(_book.IsReserved);
            Assert.AreNotEqual(expected, result);
        }

        [TestMethod]
        public void TestCancelReservation_When_DataIsIncorrect()
        {

        }


        [TestMethod]
        public void TestCancleReservation_When_DataIsCorrect()
        {

        }




    }
}