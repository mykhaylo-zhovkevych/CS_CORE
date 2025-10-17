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

            _policy = new Policy { PolicyName = "Student_BoardGame__policy", UserType = UserType.Student, ItemType = ItemType.VideoGame };
            _policy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriod: 30);

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
            PolicyService.AddPolicy(_policy);

            // Assert
            Assert.AreEqual(_policy, PolicyService.GetPolicy(_policy.UserType, _policy.ItemType));

        }
            

        [TestMethod]
        public void TestAddPolicy_When_KeyIsIncorrect()
        {
            // Arrange
             Policy _incorrectPolicy;
             _incorrectPolicy = new Policy { PolicyName = "Student_Offocer_policy", UserType = UserType.Student, ItemType = ItemType.VideoGame };
             _incorrectPolicy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriod: 30);
            PolicyService.AddPolicy(_incorrectPolicy);


            // Assert & Act
            Assert.IsFalse(PolicyService.AddPolicy(_incorrectPolicy));
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
            PolicyService.AddPolicy(_policy);

            var result = PolicyService.GetPolicy(UserType.Student, ItemType.VideoGame);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<UserType>(_defaultStudent.UserType, result.UserType);
            Assert.AreEqual<ItemType>(_defaultVideoGame.ItemType, result.ItemType);
        }


        [TestMethod]
        public void TestUpdatePolicyValues_When_Iscorrect()
        {
            // Arrange 


            var prepare = PolicyService.GetPolicy(_defaultStudent.UserType, _defaultVideoGame.ItemType);
            PolicyService.AddPolicy(prepare);
            // Act
            var result = PolicyService.UpdatePolicyValues(_defaultStudent.UserType, _defaultVideoGame.ItemType, 5, 200.0m, 60);

            // Assert
            Assert.AreNotEqual(prepare, result);

        }

        [TestMethod]
        public void TestUpdate_policyValues_When_IsIncorrect()
        {
            // Arrange 
            var wrongPolicy = new Policy { PolicyName = "Student_BoardGame__policy", UserType = UserType.Teacher, ItemType = ItemType.VideoGame };

            // Act
            wrongPolicy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriod: 30);

            // Assert
            Assert.ThrowsException<NonExistingPolicyException>(() => PolicyService.UpdatePolicyValues(wrongPolicy.UserType, wrongPolicy.ItemType, 5, 200.0m, 60));
        }
    }
}