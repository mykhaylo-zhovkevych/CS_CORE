using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;
using System.Xml.Linq;

namespace TestTdd
{
    [TestClass]
    public sealed class LibraryMainTest
    {
        private Library _library;
        private UserManagmentDepartment _userManagement;

        [TestInitialize]
        public void Setup()
        {
            _library = new Library("Main Library", "123 Main St");
            _userManagement = new UserManagmentDepartment(_library);
            _library.AddShelf(new Shelf(1000));

            PolicyService.AddPolicy(UserType.ExternalUser, ItemType.BoardGame, new Policy("ExternalUser-BoardGame", extensions: 3, loanFees: 1.50m, loanPeriodInDays: 21));
            PolicyService.AddPolicy(UserType.Teacher, ItemType.Magazine, new Policy("Teacher-Magazine", extensions: 1, loanFees: 3.00m, loanPeriodInDays: 7));
            PolicyService.AddPolicy(UserType.Student, ItemType.Book, new Policy("Student-Book", extensions: 2, loanFees: 0.00m, loanPeriodInDays: 14));
        }

        [TestMethod]
        public void TestUpdateUserProfil()
        {
            // Arrange
            var user = new User("Test User", UserType.Student);
            var invalidUser = new User("Invalid User", UserType.ExternalUser);
            _library.Users.Add(user);

            // Act
            var changedUser = _userManagement.UpdateUserProfile(user, UserType.Teacher);
            var allEntries = _library.Users.Select(u => u.Name == changedUser.Name);

            // Assert
            Assert.AreEqual(UserType.Teacher, changedUser.UserType);
            Assert.IsTrue(user.UserType != UserType.Officer);
            Assert.IsTrue(allEntries.Count() > 0);
        }
    }
}
