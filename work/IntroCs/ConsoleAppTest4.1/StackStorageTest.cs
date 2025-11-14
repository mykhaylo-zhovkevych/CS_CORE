using ConsoleApp4._1;

namespace ConsoleAppTest4._1
{
    [TestClass]
    public class StackStorageTest
    {
        private StackStorage<int> _cellarInt;
        private StackStorage<string> _cellarString;

        [TestInitialize] 
        public void Setup()
        {
            _cellarInt = new StackStorage<int>(5);
            _cellarString = new StackStorage<string>(3);

        }

        [TestMethod]
        public void TestStackStorage_WithDifferentDataTypes_AndCorrentSize()
        {
            // Arrange & Act
            for (int i = 1; i <= 5; i++)
            {
                _cellarInt.Push(i);
            }

            for (int i = 1; i <= 3; i++)
            {
                _cellarString.Push("Item" + i);
            }

            // Assert
            Assert.IsInstanceOfType(_cellarString, typeof(StackStorage<string>));
            Assert.IsInstanceOfType(_cellarInt, typeof(StackStorage<int>));

        }

        [TestMethod]
        public void TestStackStorage_WhenStackSize_IsOveraken()
        {
            // Assert & Act
            Assert.ThrowsException<StackExceptions.StackFullException>(() =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    _cellarInt.Push(i);
                }
            });
        }

        [TestMethod]
        public void TestPop_WhenStack_IsEmpty()
        {
            Assert.ThrowsException<StackExceptions.StackEmptyException>(() =>
            {
                _cellarInt.Pop();
            });
        }

        // -
        [TestMethod]
        public void TestPop()
        {
            // Arrange
            _cellarString.Push("First");
            _cellarString.Push("Second");

            Assert.AreEqual("Second", _cellarString.Pop());
            Assert.AreEqual("First", _cellarString.Pop());


        }


        [TestMethod]
        public void TestPush_WhenStack_IsFull()
        {
            Assert.ThrowsException<StackExceptions.StackFullException>(() =>
            {
                _cellarString.Push("First");
                _cellarString.Push("Second");
                _cellarString.Push("Third");
                _cellarString.Push("Fourth");
            });
        }


        [TestMethod]
        public void TestPush()
        {
            // Arrange & Act
            _cellarInt.Push(10);
            _cellarInt.Push(20);

            Assert.AreEqual(20, _cellarInt.Pop());
            Assert.AreEqual(10, _cellarInt.Pop());
      
        }

        [TestMethod]
        public void TestToString_IfReturnsInReverseOrder()
        {
            // Arrange
            var stack = new StackStorage<string>();
            stack.Push("A");
            stack.Push("B");
            stack.Push("C");

            // Act
            string result = stack.ToString();

            string expected = "C\nB\nA\n";

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
