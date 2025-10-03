using ConsoleApp5._2;
using System.Text.RegularExpressions;

namespace ConsoleAppTest._5._2
{
    [TestClass]
    public sealed class GeneratorTest
    {
        private Generator _gen;

        [TestInitialize]
        public void Setup()
        {
            _gen = new Generator();
        }


        [TestMethod]
        public void TestGenerateString_WithDefaultValue()
        {
            // Arrange
            string random;
            int expectedLength = 23;

            // Act
            random = _gen.GenerateString(expectedLength);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(random));
            Assert.AreEqual(expectedLength, random.Length);

        }

        [TestMethod]
        public void TestGenerateString_WithCustomeValue()
        {
            // Arrange
            string value = "ABCDfkpq";
            string random;
            // Regex.Escape is used to prevent from falsely interpreting signs as regex
            string pattern = $"^[{Regex.Escape(value)}]+$";

            // Act
            random = _gen.GenerateString(23, value);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(random));
            StringAssert.Matches(random, new Regex(pattern));
        }

        [TestMethod]
        public void TestGenerateString_WithRandomValues()
        {
            // Arrange
            string value = "ABC";

            // Act
            string r1 = _gen.GenerateString(20, value);
            string r2 = _gen.GenerateString(20, value);

            // Assert
            Assert.AreNotEqual(r1, r2, "If not equal then they are random");
            
        }

        [TestMethod]
        public void TestGenerateString_WithNullValue()
        {
            // Arrange
            string value = null;
            string random;
            const string DefaultAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string pattern = $"^[{Regex.Escape(DefaultAlphabet)}]+$";

            // Act
            random = _gen.GenerateString(23, value);

            // Assert
            Assert.IsNotNull(random, "If not null than pass");
            StringAssert.Matches(random, new Regex(pattern));

        }

    }
}
