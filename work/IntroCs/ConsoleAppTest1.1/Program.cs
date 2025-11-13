namespace ConsoleAppTest1._1
{
    [TestClass]
    public class Program
    {

        [TestMethod]
        public void When_Denominator_IsPassedAsZero()
        {
            // Arrange
            var f1 = new Fractions(1, 3);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => f1.Subtract(new Fractions(1, 0)));

        }

        [TestMethod]
        public void TestAdd_TwoFractions()
        {
            // Arrange
            var f1 = new Fractions(1, 3);
            var d2 = new Fractions(3, 1);

            var expected = new Fractions(10, 3);

            // Act
            var result = f1.Add(d2);

            Assert.AreEqual(expected.Numerator, result.Numerator);
            Assert.AreEqual(expected.Denominator, result.Denominator);

        }

        [TestMethod]
        public void TestAdd_TwoNegativeFactions()
        {
            // Arrange
            var nf1 = new Fractions(-2, 10);
            var nf2 = new Fractions(-3, 4);

            var expected = new Fractions(-38, 40);

            // Act
            var result = nf1.Add(nf2);

            // Assert
            Assert.AreEqual(expected.Numerator, result.Numerator);
            Assert.AreEqual(expected.Denominator, result.Denominator);

        }


        [TestMethod]
        public void TestSubtract_TwoFractions()
        {
            // Arrange
            var f1 = new Fractions(1, 3);
            var d2 = new Fractions(3, 1);

            var expected = new Fractions(-8, 3);

            // Act
            var result = f1.Subtract(d2);

            Assert.AreEqual(expected.Numerator, result.Numerator);
            Assert.AreEqual(expected.Denominator, result.Denominator);

        }

        [TestMethod]
        public void TestSubtract_TwoNegativeFactions()
        {
            // Arrange
            var nf1 = new Fractions(-2, 10);
            var nf2 = new Fractions(-3, 4);

            var expected = new Fractions(22, 40);

            // Act
            var result = nf1.Subtract(nf2);

            // Assert
            Assert.AreEqual(expected.Numerator, result.Numerator);
            Assert.AreEqual(expected.Denominator, result.Denominator);

        }

        [TestMethod]
        public void TestExpand_TwoFraction()
        {
            // Arrange
            var f1 = new Fractions(2, 10);
            var expected = new Fractions(10, 50);

            // Act
            var result = f1.Expand(5);

            // Assert
            Assert.AreEqual(expected.Numerator, result.Numerator);

        }

        [TestMethod]
        public void TestExpand_TwoNegativeFraction()
        {
            // Arrange
            var f1 = new Fractions(-2, 10);
            var expected = new Fractions(-10, 50);

            // Act
            var result = f1.Expand(5);

            // Assert
            Assert.AreEqual(expected.Numerator, result.Numerator);
        }

        [TestMethod]
        public void TestExpand_IfWrongType_TwoFraction()
        {
            // Arrange
            var f1 = new Fractions(2, 10);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => f1.Expand(0));
        }

        [TestMethod]
        public void TestReduce_TwoFraction()
        {
            // Arrange
            var f1 = new Fractions(2, 10);
            var expected = new Fractions(1, 5);
            // Act
            var result = f1.Reduce();
            // Assert
            Assert.AreEqual(expected.Numerator, result.Numerator);
            Assert.AreEqual(expected.Denominator, result.Denominator);

        }

        [TestMethod]
        public void TestReduce_UnreducibleFraction()
        {
            // Arrange 
            var f1 = new Fractions(2, 3);
            var expected = new Fractions(2, 3);

            // Act
            var result = f1.Reduce();
            // Assert
            Assert.AreEqual(expected.Numerator, result.Numerator);
            Assert.AreEqual(expected.Denominator, result.Denominator);

        }
    }
}
