using ConsoleApp5._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleAppTest._5._2
{
    [TestClass]
    public class ExtentionMethodsTest
    {
        private string _testString;
             
        [TestInitialize]
        public void Setup()
        {
            _testString = "Hello this is a Demo string!";
        }

        [TestMethod]
        public void TestExtentionMethods_WhenReverse_IsUsed()
        {
            // Arrange
            string reversed;

            // Act
            reversed = _testString.ReverseInput();
            string expectedValue = new string(_testString.Reverse().ToArray());

            // Assert
            Assert.AreEqual(expectedValue, reversed);

        }

        [TestMethod]
        public void TestExtentionMethods_WhenReverse_IsNull()
        {
            // Arrange
            string? invalidInput = null;

            // Act & Assert            
            Assert.ThrowsException<ArgumentNullException>(() => invalidInput.ReverseInput());
        }

        [TestMethod]
        public void TestExtentionMethods_WhenSnakeCaseInput_IsUsed()
        {
            // Arrange
            string changedOutput;
            string correctOutput = "hello_this_is_a_demo_string";

            // Act
            changedOutput = _testString.SnakeCaseInput();

            // Assert
            StringAssert.StartsWith(changedOutput, "hello");
            StringAssert.Contains(changedOutput, "_");
            StringAssert.Equals(changedOutput, correctOutput);


        }

        [TestMethod]
        public void TestExtentionMethods_WhenSnakeCaseInput_IsNull()
        {
            // Arrange
            string? invalidInput = null;

            // Act & Assert            
            Assert.ThrowsException<ArgumentNullException>(() => invalidInput.SnakeCaseInput());
        }

        [TestMethod]
        public void TestExtentionMethods_WhenTruncateInput_IsUsed()
        {
            // Arrange
            string shortenedOutput;
            string expectedShortenedOutput = "Hello this is a...";

            // Act
            shortenedOutput = _testString.TruncateInput(15);

            // Assert
            StringAssert.Contains(shortenedOutput, "...");
            Assert.AreEqual(shortenedOutput, expectedShortenedOutput);
            Assert.AreEqual(expectedShortenedOutput.Length, shortenedOutput.Length);

        }


        [TestMethod]
        public void TestExtentionMethods_WhenTruncateInput_IsNull()
        {
            // Arrange
            string? invalidInput = null;

            // Act & Assert            
            Assert.ThrowsException<ArgumentNullException>(() => invalidInput.TruncateInput(15));
        }
    }
}
