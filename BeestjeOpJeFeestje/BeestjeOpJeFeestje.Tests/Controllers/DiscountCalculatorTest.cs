using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BeestjeOpJeFeestje.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DiscountCalculatorTest
    {
        private DiscountCalculator _calc;

        [TestMethod]
        public void TypeDiscount_DiscountTrue_Test()
        {
            //1. Arange
            _calc = new DiscountCalculator();

            var beasts = new List<Beast>
            {
                new Beast
                {
                    Name = "Koe",
                    Price = 100,
                    Type = "Boerderij"
                },
                new Beast
                {
                    Name = "Paard",
                    Price = 100,
                    Type = "Boerderij"
                },
                new Beast
                {
                    Name = "Varken",
                    Price = 100,
                    Type = "Boerderij"
                }
            };

            //2. Act
            var result = _calc.TypeDiscount(beasts);

            //3. Assert
            Assert.AreEqual(10, result.PercentageDiscount);
        }

        [TestMethod]
        public void TypeDiscount_DiscountFalse_Test()
        {
            //1. Arange
            _calc = new DiscountCalculator();

            var beasts = new List<Beast>
            {
                new Beast
                {
                    Name = "Koe",
                    Price = 100,
                    Type = "Boerderij"
                },
                new Beast
                {
                    Name = "Paard",
                    Price = 100,
                    Type = "Boerderij"
                },
                new Beast
                {
                    Name = "Hagedis",
                    Price = 200,
                    Type = "Woestijn"
                }
            };

            //2. Act
            var result = _calc.TypeDiscount(beasts);

            //3. Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CalculateCharacterDiscount_DiscountFalse_Test()
        {
            //1. Arrange
            _calc = new DiscountCalculator();
            const string bono = "bono";
            const int bonoDiscountShouldBe = 0;

            //2. Act
            var actualResult = _calc.CalculateCharacterDiscount(bono);

            //3. Assert
            Assert.AreEqual(bonoDiscountShouldBe, actualResult);
        }

        [TestMethod]
        public void CalculateCharacterDiscount_DiscountTrue_Test()
        {
            //1. Arrange
            _calc = new DiscountCalculator();
            const string abc = "abc";
            const int abcDiscountShouldBe = 6;

            //2. Act
            var actualResult = _calc.CalculateCharacterDiscount(abc);

            //3. Assert
            Assert.AreEqual(abcDiscountShouldBe, actualResult);
        }

        [TestMethod]
        public void DuckDiscount_DuckDiscountTrue_Test()
        {
            //1. Arrange
            _calc = new DiscountCalculator();
            const string duckName = "Eend"; 
            const int expectedDuckDiscount = 50;
            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(6)).Returns(1);

            //2. Act
            var discount = _calc.DuckDiscount(duckName, randomMock.Object.Next(6));

            //3. Assert
            Assert.AreEqual(expectedDuckDiscount, discount.PercentageDiscount);
        }

        [TestMethod]
        public void DuckDiscount_DuckDiscountFalse_Test()
        {
            //1. Arrange
            _calc = new DiscountCalculator();
            const string duckName = "Eend";
            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(6)).Returns(3);

            //2. Act
            var discount = _calc.DuckDiscount(duckName, randomMock.Object.Next(6));

            //3. Assert
            Assert.IsNull(discount);
        }

        [TestMethod]
        public void DateDiscount_DiscountTrue_Test()
        {
            //1. Arrange
            _calc = new DiscountCalculator();
            const int expectedDateDiscount = 15;
            var monday = new DateTime(2020, 01, 06);
            var tuesday = new DateTime(2020, 01, 07);

            //2. Act
            var resultMonday = _calc.DateDiscount(monday);
            var resultTuesday = _calc.DateDiscount(tuesday);

            //3. Assert
            Assert.AreEqual(expectedDateDiscount, resultMonday.PercentageDiscount);
            Assert.AreEqual(expectedDateDiscount, resultTuesday.PercentageDiscount);
        }

        [TestMethod]
        public void DateDiscount_DiscountFalse_Test()
        {
            //1. Arrange
            _calc = new DiscountCalculator();
            var wednesday = new DateTime(2020, 01, 08);
            var thursday = new DateTime(2020, 01, 09);
            var friday = new DateTime(2020, 01, 10);
            var saturday = new DateTime(2020, 01, 11);
            var sunday = new DateTime(2020, 01, 12);

            //2. Act
            var resultWednesday = _calc.DateDiscount(wednesday);
            var resultThursday = _calc.DateDiscount(thursday);
            var resultFriday = _calc.DateDiscount(friday);
            var resultSaturday = _calc.DateDiscount(saturday);
            var resultSunday = _calc.DateDiscount(sunday);

            //3. Assert
            Assert.IsNull(resultWednesday);
            Assert.IsNull(resultThursday);
            Assert.IsNull(resultFriday);
            Assert.IsNull(resultSaturday);
            Assert.IsNull(resultSunday);
        }

        [TestMethod]
        public void PartialDiscountShouldbeTen_Test()
        {
            //1. Arrange
            _calc = new DiscountCalculator();
            const int ExpectedPartialDiscount = 10;

            //2. Act
            var actualResult = _calc.CalculateHalvedDiscount(80, 30);
            
            //3. Assert.
            Assert.AreEqual(ExpectedPartialDiscount, actualResult);
        }
    }
}