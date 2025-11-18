using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;

namespace TestTdd
{
    [TestClass]
    public sealed class LibraryMainTest
    {

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
    }
}
