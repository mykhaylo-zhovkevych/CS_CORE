using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;
using ConsoleApp5._4Remastered.Exceptions;
using ConsoleApp5._4Remastered.HelperClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ConsoleAppTest5._4Remastered
{

    [TestClass]
    [DoNotParallelize]
    public class LibraryTest
    {
        private  Library _library;
        private  Policy _policy;

        private  User _teacher;
        private  User _student;
        private  Item _book;
        private  Item _videoGame;

        [TestInitialize]
        public void Setup( )
        {

            _library = new Library("Central Library", "123 Main St.");

            _student = new User("TestName", UserType.Student);
            _teacher = new User("TestName02", UserType.Teacher);

            _book = new Item("CS Book", ItemType.Book);
            _videoGame = new Item("TestVideoGameOne", ItemType.VideoGame);


            var teacherPolicy = new Policy { PolicyName = "Teacher_VideoGame_Policy" };
            teacherPolicy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriodInDays: 14);
            PolicyService.AddPolicy(UserType.Teacher, ItemType.VideoGame, teacherPolicy);

            var studentPolicy = new Policy { PolicyName = "Student_VideoGame_Policy" };
            studentPolicy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriodInDays: 21);
            PolicyService.AddPolicy(UserType.Student, ItemType.VideoGame, teacherPolicy);
        }

        [TestCleanup]
        public void ClassCleanup()
        {
            PolicyService.ClearPolicies();
        }


        [TestMethod]
        public void BorrowItem_When_DataIsCorrect()
        {

            // Act
            var result = _library.BorrowItem(_student, _videoGame);

            // Assert
            Assert.IsTrue(result.Item1);
            Assert.IsTrue(_videoGame.IsBorrowed);
            Assert.AreEqual(1, _library.Borrowings.Count);
        }

  
        [TestMethod]
        public void BorrowItem_When_NoPolicyFound()
        {
            // Assert & Act
            Assert.ThrowsException<NonExistingPolicyException>(() => _library.BorrowItem(_student, _book));
        }

        [TestMethod]
        public void BorrowItem_When_DataIsWrong()
        {
            // Arrange
            _book.IsBorrowed = true;

            // Assert & Act
            Assert.ThrowsException<IsAlreadyBorrowedException>(() => _library.BorrowItem(_student, _book));

        }

        

        [TestMethod]
        public void TestReturnItem_When_DataIsCorrect()
        {
            // Arrange 
            _library.BorrowItem(_student, _videoGame);

            // Act
            var result = _library.ReturnItem(_student, _videoGame);  

            // Assert
            Assert.IsTrue(result.Item1);
            Assert.IsFalse(_book.IsBorrowed);
            var borrowing = _library.Borrowings.First();
            Assert.IsNotNull(borrowing.ReturnDate);

        }

        [TestMethod]
        public void TestReturnItem_When_DataNotFound()
        {
            // Arramge
            _library.BorrowItem(_student, _videoGame);
            var anotherBook = new Item("Another VideoGame", ItemType.VideoGame);

            // Assert & Act 
            Assert.ThrowsException<System.ArgumentException>(() => _library.ReturnItem(_student, anotherBook));
        }


        [TestMethod]
        public void TestReserveItem_When_DataIsCorrect()
        {
            // Act
            _library.BorrowItem(_student, _videoGame);
            _library.ReserveItem(_teacher, _videoGame);

            // Assert
            Assert.IsTrue(_videoGame.IsReserved);
            Assert.AreEqual(_videoGame.ReservedBy, _teacher);
        }

        [TestMethod]
        public void TestReserveItem_When_DataIsWrong()
        {
            // Arrange

            _library.BorrowItem(_student, _videoGame);
            _library.ReserveItem(_teacher, _videoGame);

            // Assert & Act
            Assert.ThrowsException<IsAlreadyReservedException>(() => _library.ReserveItem(_student, _videoGame));
            
        }

        [TestMethod]
        public void TestCancelReservation_When_DataNotFound()
        {
            // Arrange & Act 
            Assert.ThrowsException<ArgumentException>(() => _library.CancelReservation(_student, _videoGame));
        }

        [TestMethod]
        public void TestCancelReservation_When_SomeOneElseReserved()
        {
            // Arrange
 
            _library.BorrowItem(_teacher, _videoGame);

            // Assert & Act
            Assert.ThrowsException<ArgumentException>(() => _library.CancelReservation(_student, _videoGame));
        }


        [TestMethod]
        public void TestCancleReservation_When_DataIsCorrect()
        {
            // Arrange & Act
            _videoGame.IsBorrowed = true;

            var reservation = _library.ReserveItem(_teacher, _videoGame);
            var result = _library.CancelReservation(_teacher, _videoGame);

            // Assert
            Assert.AreEqual(_book.ReservedBy is null, result.Item1);
        }

        [TestMethod]
        public void TestExtendBorrowing_When_DataNotFound()
        {
            // Arrange
            _library.BorrowItem(_teacher, _videoGame);

            // Assert & Act
            Assert.ThrowsException<ArgumentException>(() => _library.ExtendBorrowingPeriod(_teacher, _book));
        }

        [TestMethod]
        public void TestExtendBorrowing_When_DataIsCorrect()
        {
            // Arrange
            _library.BorrowItem(_student, _videoGame);
            // Act
            var result = _library.ExtendBorrowingPeriod(_student, _videoGame);
            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void TestExtendBorrowing_If_DateTimeCorrectUpdated()
        {
            // Arrange & Act
            _library.BorrowItem(_student, _videoGame);
            _library.ExtendBorrowingPeriod(_student, _videoGame);

            // Assert
            var borrowing = _library.Borrowings.First(b => b.User.Id == _student.Id &&
                b.Item.Id == _videoGame.Id &&
                !b.IsReturned);

            var temp = DateTime.Today.AddDays(14);

            Assert.IsTrue(borrowing.DueDate >= temp);
            Assert.IsTrue(borrowing.LoanDate < borrowing.DueDate);
            Assert.AreEqual(borrowing.DueDate, DateTime.Today.AddDays(14));
            Assert.AreEqual(borrowing.LoanDate, DateTime.Today);
        }

        [TestMethod]
        public void TestExtendBorrowing_Is_CreditsDecrement()
        {
            // Arrange & Act
            _library.BorrowItem(_student, _videoGame);
            _library.ExtendBorrowingPeriod(_student, _videoGame);

            // Assert
            var borrowing = _library.Borrowings.First(b => b.User.Id == _student.Id &&
                b.Item.Id == _videoGame.Id &&
                !b.IsReturned);


            Assert.IsTrue(borrowing.RemainingExtensionCredits == 1);

        }


        [TestMethod]
        public void ReturnItem_Is_EvenCalled()
        {
            // Arrange
            _library.BorrowItem(_teacher, _videoGame);

            var borrowed = _library.Borrowings.FirstOrDefault(b =>
                b.User.Id == _teacher.Id &&
                b.Item.Id == _videoGame.Id &&
                b.ReturnDate == null);

            _library.ReserveItem(_student, _videoGame);

            bool eventRaised = false;
            ItemEventArgs? eventArgs = null;

            _library.InformReserver += (s, e) =>
            {
                eventRaised = true;
                eventArgs = e;
         
            };

            // Act
            _library.ReturnItem(_teacher, _videoGame);

            // Assert
            Assert.IsNotNull(borrowed);
            Assert.IsNotNull(eventArgs);
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(_videoGame, eventArgs.Item);

        }


        //[TestMethod]
        //public void QueryItems_With_DataExist()
        //{
        //    // Arrange
        //    _videoGame.IsBorrowed = true;
        //    _videoGame.ReservedBy = _teacher;

        //    // 

        //    // Act
        //    var result = _library.QueryItems(
        //        nameContains: _videoGame.Name,
        //        isBorrowed: true,
        //        isReserved: true,
        //        customPredicate: i => i.ItemType == ItemType.VideoGame);

        //    // Assert
        //    Assert.IsTrue(result.Any());
        //    Assert.AreEqual(1, result.Count());

        //}

        //[TestMethod]
        //public void QueryItems_With_DataDontExist()
        //{
        //    // Arrange

        //    _videoGame.IsBorrowed = false;
        //    _videoGame.ReservedBy = _student;

        //    // Act
        //    var result = _library.QueryItems(
        //        nameContains: _videoGame.Name,
        //        isBorrowed: true,
        //        isReserved: true,
        //        customPredicate: i =>  i.ItemType == ItemType.Book);

        //    // Assert
        //    Assert.AreNotEqual(1, result.Count());
        //    Assert.IsFalse(result.Any());

        //}
    }
}