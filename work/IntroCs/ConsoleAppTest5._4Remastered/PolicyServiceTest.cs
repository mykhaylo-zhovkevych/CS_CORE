using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;

namespace ConsoleAppTest5._4Remastered
{
    [TestClass]
    public class _policyServiceTest
    {
        private Policy _policy;
        private User _defaultStudent;
        private Item _defaultBoardGame;

        [TestInitialize]
        public void Setup()
        {
            _defaultStudent = new User("Default Student", UserType.Student);
            _defaultBoardGame = new Item("Default BoardGame", ItemType.BoardGame);

            _policy = new Policy { PolicyName = "Student_BoardGame__policy", User = _defaultStudent, Item = _defaultBoardGame };
            //_policy = new _policy { _policyName = "Student_BoardGame__policy", User = null, Item = _defaultBoardGame };

            _policy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriod: 30);

        }

        [TestMethod]
        public void TestAdd_policy_When_KeyIsCorrect()
        {  
            // Arrange & Act
            PolicyService.AddPolicy(_policy);

            // Assert
            Assert.IsTrue(_policy.User.Name == _defaultStudent.Name);

        }

        [TestMethod]
        public void TestAdd_policy_When_KeyIsIncorrect()
        {
            // Assert & Act
            Assert.ThrowsException<NullReferenceException>(() => PolicyService.AddPolicy(_policy));
        }

        [TestMethod]
        public void TestGet_policy_If_NoKeyIsPresent()
        {
            // Assert & Act
            Assert.ThrowsException<NullReferenceException>(() => PolicyService.GetPolicy(_defaultStudent, null));
        }

        [TestMethod]
        public void TestGet_policy_If_KeyIsPresent()
        {
            // Arrange & Act
            var result = PolicyService.GetPolicy(_defaultStudent, _defaultBoardGame);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<UserType>(_defaultStudent.UserType, result.User.UserType);
            Assert.AreEqual<ItemType>(_defaultBoardGame.ItemType, result.Item.ItemType);
        }


        [TestMethod]
        public void TestUpdate_policyValues_When_KeyIscorrect()
        {
            // Arrange 
            var prepare = PolicyService.AddPolicy(_policy);

            // Act
            var result = PolicyService.UpdatePolicyValues(_defaultStudent, _defaultBoardGame, 5, 200.0m, 60);

            // Assert
            Assert.AreNotEqual(prepare, result);

        }

        [TestMethod]
        public void TestUpdate_policyValues_When_KeyIsIncorrect()
        {
            // Arrange 
            var wrongPolicy = new Policy { PolicyName = "Student_BoardGame__policy", User = null, Item = _defaultBoardGame };

            // Act
            wrongPolicy.SetValues(extensions: 2, loanFees: 50.0m, loanPeriod: 30);

            // Assert
            Assert.ThrowsException<NullReferenceException>(() => PolicyService.UpdatePolicyValues(wrongPolicy.User, wrongPolicy.Item, 5, 200.0m, 60));
        }
    }
}