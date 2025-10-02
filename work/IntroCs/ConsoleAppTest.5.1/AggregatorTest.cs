using ConsoleApp5._1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleAppTest._5._1
{
    [TestClass]
    public sealed class AggregatorTest
    {

        [TestMethod]
        public void TestCalculate_MustInvokeEventWithCorrectResult()
        {

            // Arrange
            var agg = new Aggregator();
            int? receivedResult = null;

            agg.ResultNewState += (s, result) => receivedResult = result;

            agg.AddNumber(2);
            agg.AddNumber(4);
            agg.AddNumber(6);

            // Act

            agg.Calculate(nums => nums.Sum());

            // Assert

            Assert.IsNotNull(receivedResult);
            Assert.AreEqual(12, receivedResult.Value);
        }

        [TestMethod]
        public void TestCalculate_WithEmptyList()
        {
            // Arrange

            var agg = new Aggregator();
            int? receivedResult = null;

            agg.ResultNewState += (s, result) => receivedResult = result;

            // Act

            agg.Calculate(nums => nums.Sum());

            // Assert

            Assert.AreEqual(0, receivedResult.Value);
        }
    }
}