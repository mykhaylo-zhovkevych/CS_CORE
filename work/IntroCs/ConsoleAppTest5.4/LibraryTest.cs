using ConsoleApp5._4;
using ConsoleApp5._4.Enum;
using ConsoleApp5._4.Exceptions;
using ConsoleApp5._4.HelperClasses;
using ConsoleApp5._4.Interface;
using ConsoleApp5._4.Items;
using ConsoleApp5._4.Users;
using System.Linq;

namespace ConsoleAppTest5._4
{

    // MethodName_Scenerio_ExpectedOutcome
    // e.x. Add_AddsTwoWholeNumbers_SumOfThoseNumbers

    [TestClass]
    public class LibraryTest
    {
        private Library _library;

        private User _student;
        private User _teacher;
        private User _admin;
   
        private Item _book;
        private Item _book02;
        private Item _videoGame;

        [TestInitialize]
        public  void ClassInit()
        {
            var policyProvider = new DefaultBorrowPolicyProvider();

            _library = new Library("Central Library", "123 Main St.", policyProvider);
            var shelf1 = new Shelf(1);
            var shelf2 = new Shelf(2);

            _student = new Student(Guid.NewGuid(), "TestStudentOne");
            _teacher = new Teacher(Guid.NewGuid(), "TestTeacherOne");
            _admin = new Admin(Guid.NewGuid(), "TestTeacherOne");

            _book = new Book( "TestBookOne", "TestPublisherOne");
            _book02 = new Book("TestBookOne", "TestPublisherTwo");
            _videoGame = new VideoGame( "TestVideoGameOne", GameType.RPG, 20);

            shelf1.AddItemToShelf(_book);
            shelf1.AddItemToShelf(_videoGame);

            _library.AddShelf(shelf1);
            _library.AddShelf(shelf2);

        }

        [TestMethod]
        public void BorrowItem_When_DataIsCorrect()
        {
            // Act
            Result<Borrowing> borrowing = (Result<Borrowing>)_library.BorrowItem(_student, _book.Name);

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
            Assert.ThrowsExactly<ArgumentException>(() => _library.BorrowItem(_teacher, "WrongData"));
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
            Assert.ThrowsExactly<NonExistingPolicyException>(() => _library.BorrowItem(_admin, _book.Name));
        }


        [TestMethod]
        public void ReturnItem_When_DataIsCorrect()
        {
            // Act
            _library.BorrowItem(_student, "TestBookOne");

            _library.ReturnItem(_student, "TestBookOne");

            // Assert
            Assert.IsFalse(_book.IsBorrowed);
        }


        [TestMethod]
        public void ReturnItem_When_DataIsWrong()
        {
            // Arrange
            var result = _library.ReturnItem(null, _book.Name);

            // Assert & Act
            Assert.IsFalse(result.Success);
            Assert.AreNotEqual(true, _book.IsBorrowed);
            Assert.ThrowsExactly<ArgumentException>(() => _library.ReturnItem(_teacher, _book.Name));

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

            _videoGame.ReservedBy = _student;

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
            _library.BorrowItem(_teacher, "TestVideoGameOne");
            _library.ReserveItem(_student, "TestVideoGameOne");

            // Assert
            Assert.IsTrue(_videoGame.IsReserved);
            Assert.AreEqual(_videoGame.ReservedBy, _student);
        }

        [TestMethod]
        public void ReserveItem_When_DataIsWrong()
        {
            // Arrange 
            var result = _library.ReserveItem(null, _videoGame.Name);

            // Assert & Act
            Assert.IsFalse(result.Success);
            Assert.ThrowsExactly<ArgumentException>(() => _library.ReserveItem(_teacher, "WrongData"));
        }

        [TestMethod]
        public void ReserveItem_When_ReservedBySomeoneElse()
        {
            // Arrange
            _book.ReservedBy = _student;
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
            _book.ReservedBy = _teacher;

            // Act
            _library.CancelReservation(_teacher, "TestBookOne");

            // Assert
            Assert.IsFalse(_book.IsReserved);
        }

        [TestMethod]
        public void CancleReservation_When_DataIsWrong()
        {
            // Assert & Act
            Assert.ThrowsExactly<ArgumentException>(() => _library.CancelReservation(_teacher, "dccsd"));

        }

        [TestMethod]
        public void CancleReservation_For_SomeoneElse()
        {
            // Arrange
            _videoGame.ReservedBy = _teacher;
            var result = _library.CancelReservation(_student, _videoGame.Name);

            // Act
            _library.CancelReservation(_student, _videoGame.Name);

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
            var oldExtensions = borrowed.ExtentionCredits;

            // Act
            _library.ExtendBorrowingPeriod(_student, _book.Name);

            var updated = _library.Borrowings.FirstOrDefault(b =>
                b.User.Id == _student.Id &&
                b.Item.Id == _book.Id &&
                b.ReturnDate == null);

            var newDue = updated.DueDate;
            var newExtensions = updated.UsedBorrowingCredits;

            // Assert
            Assert.IsNotNull(borrowed);
            Assert.IsNotNull(updated);
            Assert.IsTrue(newDue > oldDue);
            Assert.AreNotEqual(newExtensions, oldExtensions);
        }

        [TestMethod]
        public void ExtendBorrowingPeriod_When_DataIsWrong()
        {
            // Arrange
            var wrongResult = _library.ExtendBorrowingPeriod(null, _book.Name);

            // Assert & Act
            Assert.IsFalse(wrongResult.Success);
            Assert.ThrowsExactly<ArgumentException>(() => (_library.ExtendBorrowingPeriod(_teacher, "wefwefwe")));
        }

        [TestMethod]
        public void ExtendBorrowingPeriod_When_IsReservedTrue()
        {
            // Arrange
            _book.ReservedBy = _teacher;

            // Assert & Act
            Assert.ThrowsExactly<IsAlreadyReservedException>(() => _library.ExtendBorrowingPeriod(_student, _book.Name));
        }

        [TestMethod]
        public void ExtendBorrowingPeriod_When_NoEnoughExtention()
        {
            // Arrange
            var policyProvider = new DefaultBorrowPolicyProvider();
            policyProvider.AddRule(typeof(Admin), typeof(VideoGame), new BorrowPolicy(21));

            var library = new Library("TestName", "TestAdress", policyProvider);

            var shelf = new Shelf(1);
            var testVideoGame = new VideoGame("TestVideoGameTwo", GameType.RPG, 20);

            shelf.AddItemToShelf(testVideoGame);
            library.AddShelf(shelf);


            // Act
            library.BorrowItem(_admin, testVideoGame.Name);
            var extensionsResult = library.ExtendBorrowingPeriod(_admin, testVideoGame.Name);

            // Assert
            Assert.IsFalse(extensionsResult.Success);
            Assert.AreEqual($"{_admin.Name} don't have enough extentions points", extensionsResult.Message);
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


        [TestMethod]
        public void QueryItems_With_DataExist()
        {
            // Arrange
            _book.IsBorrowed = true;
            _book.ReservedBy = _student;

            _book02.IsBorrowed = true;
            _book02.ReservedBy = _teacher;

            // Act
            var result = _library.QueryItems(
                nameContains: "TestBookOne",
                isBorrowed: true,
                isReserved: true,
                customPredicate: i => (i is Book b) && b.Publisher.Equals("TestPublisherOne"));
      

            // Assert
            Assert.AreEqual(1,result.Count());
            Assert.AreEqual(_book, result.First());

        }

        [TestMethod]
        public void QueryItems_With_DataDontExist()
        {
            // Arrange
            _book.IsBorrowed = false;
            _book.ReservedBy = _student;

            _book02.IsBorrowed = true;
            _book02.ReservedBy = _teacher;

            // Act
            var result = _library.QueryItems(
                nameContains: "TestBookOne",
                isBorrowed: true,
                isReserved: true,
                customPredicate: i => (i is Book b) && b.Publisher.Equals("TestPublisherOne"));

            // Assert
            Assert.AreNotEqual(1, result.Count());
            Assert.IsEmpty(result);
            
        }
    }
}