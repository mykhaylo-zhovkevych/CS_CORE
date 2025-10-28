using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;
using ConsoleApp5._4Remastered.Exceptions;

namespace ConsoleAppTest5._4Remastered
{
    [TestClass]
    [DoNotParallelize]
    public class _policyServiceTest
    {
        private static Policy _policy;
        private static User _defaultStudent;
        private static Item _defaultVideoGame;

        [TestInitialize]
        public void Setup()
        {
            _defaultStudent = new User("Default Student", UserType.Student);
            _defaultVideoGame = new Item("Default BoardGame", ItemType.VideoGame);

            _policy = new Policy {PolicyName = "Student_BoardGame__policy" };
            _policy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriodInDays: 30);

        }

        [TestCleanup]
        public void ClassCleanup()
        {
            PolicyService.ClearPolicies();
        }


        [TestMethod]
        public void TestAdd_policy_When_KeyIsCorrect()
        {  
            // Arrange & Act
            PolicyService.AddPolicy(UserType.Teacher, ItemType.VideoGame, _policy);

            // Assert
            Assert.AreEqual(_policy, PolicyService.GetPolicy(UserType.Teacher, ItemType.VideoGame));

        }
            

        [TestMethod]
        public void TestAddPolicy_When_KeyIsIncorrect()
        {
            // Arrange
             Policy _incorrectPolicy;
             _incorrectPolicy = new Policy {PolicyName = "Student_Offocer_policy" };
             _incorrectPolicy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriodInDays: 30);
            PolicyService.AddPolicy(UserType.Teacher, ItemType.VideoGame, _incorrectPolicy);


            // Assert & Act
            Assert.IsFalse(PolicyService.AddPolicy(UserType.Teacher, ItemType.VideoGame, _incorrectPolicy));
        }

        [TestMethod]
        public void TestGet_policy_If_NoKeyIsPresent()
        {
            // Assert & Act
            Assert.ThrowsException<ConsoleApp5._4Remastered.Exceptions.NonExistingPolicyException>(() => PolicyService.GetPolicy(_defaultStudent.UserType, ItemType.Magazine));
        }

        [TestMethod]
        public void TestGetPolicy_If_KeyIsPresent()
        {
            // Arrange & Act
            PolicyService.AddPolicy(UserType.Student, ItemType.VideoGame, _policy);

            var result = PolicyService.GetPolicy(UserType.Student, ItemType.VideoGame);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<UserType>(_defaultStudent.UserType, UserType.Student);
            Assert.AreEqual<ItemType>(_defaultVideoGame.ItemType, ItemType.VideoGame);
        }


        [TestMethod]
        public void TestUpdatePolicyValues_When_Iscorrect()
        {
            // Assert & Act
            Assert.ThrowsException<NonExistingPolicyException>(() => PolicyService.GetPolicy(UserType.Teacher, ItemType.VideoGame));

        }

        [TestMethod]
        public void TestUpdate_policyValues_When_IsIncorrect()
        {
            // Arrange 
            var wrongPolicy = new Policy {PolicyName = "Student_BoardGame__policy" };

            // Act
            wrongPolicy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriodInDays: 30);

            // Assert
            Assert.ThrowsException<NonExistingPolicyException>(() => PolicyService.UpdatePolicyValues(UserType.Teacher, ItemType.VideoGame, 5, 200.0m, 60));
        }
    }
}