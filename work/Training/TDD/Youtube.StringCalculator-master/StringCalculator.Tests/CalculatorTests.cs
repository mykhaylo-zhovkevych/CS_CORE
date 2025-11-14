using System;
using System.Net.Security;
using FluentAssertions;
using Xunit;

namespace StringCalculator.Tests
{
    public class CalculatorTests
    {
        //[Theory]
        //[InlineData("", 0)]
        //[InlineData("1", 1)]
        //[InlineData("1,2", 3)]
        //public void Add_AddsUpToTwoNumber_whenStringIsValid(string calculation, int expected)
        //{
        //    // Arrange
        //    var sun = new Calculator();


        //    // Act
        //    var result = sun.Add(calculation);


        //    // Assert
        //    result.Should().Be(expected);
        //}

        [Theory]
        [InlineData("1\n,2,3", 6)]
        [InlineData("10\n,90,10,\n20", 130)]
        public void Add_AddsUpToAnyNumber_WhenStringIsValid(string calculation, int expected)
        {
            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(calculation);

            // Assert
            result.Should().Be(expected);
        }


        [Theory]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//;\n1;2;4", 7)]
        public void Add_AddsNumbersUsingCustomDelimiter_WhenStringIsInvalid(string calculation, int expected)
        {

            // Arrange
            var sut = new Calculator();

            // Act
            var result = sut.Add(calculation);

            // Assert
            result.Should().Be(expected);

        }

        [Theory]
        [InlineData("1,2,-1", "-1")]
        [InlineData("//;\n1;-2;-4", "-2,-4")]
        public void Add_ShouldThrowAnException_WhenNegativeNumbersAreUsed(string calculation, string negativeNubers)
        {

            // Arrange
            var sut = new Calculator();

            // Act
            Action action = () => sut.Add(calculation);

            // Assert
            action.Should().Throw<Exception>().WithMessage("Negatives not allowed: " +negativeNubers);

        }



    }
}