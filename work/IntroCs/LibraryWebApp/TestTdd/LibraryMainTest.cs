using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;

namespace TestTdd
{
    [TestClass]
    public sealed class LibraryMainTest
    {
        private Library _library;

        [TestInitialize]
        public void Setup()
        {
            _library = new Library("Main Library", "123 Main St");
            _library.AddShelf(new Shelf(1000));

            PolicyService.AddPolicy(UserType.ExternalUser, ItemType.BoardGame, new Policy("ExternalUser-BoardGame", extensions: 3, loanFees: 1.50m, loanPeriodInDays: 21));
            PolicyService.AddPolicy(UserType.Teacher, ItemType.Magazine, new Policy("Teacher-Magazine", extensions: 1, loanFees: 3.00m, loanPeriodInDays: 7));
            PolicyService.AddPolicy(UserType.Student, ItemType.Book, new Policy("Student-Book", extensions: 2, loanFees: 0.00m, loanPeriodInDays: 14));



        }

        // User can change his user profile (type)
        [TestMethod]
        public void TestChangeUserType()
        {
            // Arrange
            var user = new User("Test User", UserType.Officer);

            // Act
            var changedUser = user.ChangeUserProfile(UserType.Teacher);

            // Assert
            Assert.AreEqual(UserType.Teacher, changedUser.UserType);
            Assert.IsTrue(user.UserType != UserType.Officer);

        }

        [TestMethod]
        public void TestUserExists()
        {
            // Arrange

        }

        [TestMethod]
        public void TestItemExists()
        {

        }

        [TestMethod]
        public void TestFindShelfById()
        {

        }

        [TestMethod]
        public void TestBorrowingIsPossible()
        {




        }



        [TestMethod]
        public void TestGetActiveBorrowingsForUser()
        {

        }


    }
}
