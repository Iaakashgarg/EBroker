using Nagarro.NAGP.EBroker.Shared.Wrappers;
using System;
using Xunit;

namespace Nagarro.NAGP.EBroker.Tests.Shared_Tests
{
    public class StaticMethodsTest
    {
        // SUT
        private readonly IWrapper wrapper;
        public StaticMethodsTest()
        {
            wrapper = new Wrapper();
        }

        [Fact]
        public void Test_IsInvalidDay_ReturnsFalse()
        {
            // Arrange
            DateTime date = new DateTime(2021, 12, 18, 09, 15, 00);

            // Act
            var output = wrapper.IsValidDate(date);

            // Assert
            Assert.False(output);
        }

        [Fact]
        public void Test_IsInvalidTime_ReturnsFalse()
        {
            // Arrange
            DateTime date = new DateTime(2021, 12, 16, 18, 15, 00);

            // Act
            var output = wrapper.IsValidDate(date);

            // Assert
            Assert.False(output);
        }

        [Fact]
        public void Test_IsValidDateTime_ReturnsTrue()
        {
            // Arrange
            DateTime date = new DateTime(2021, 12, 16, 10, 25, 00);

            // Act
            var output = wrapper.IsValidDate(date);

            // Assert
            Assert.True(output);
        }

        [Fact]
        public void TestGetUpdatedFund_ReturnsUpdatedFundMinusOvercharge()
        {
            // Arrange
            double fund = 110000;

            // Act
            double output = wrapper.GetUpdatedFund(fund);

            // Assert
            Assert.True(output == 109945);
        }

        [Theory]
        [InlineData(5000)]
        [InlineData(500)]
        [InlineData(1500)]
        [InlineData(78)]
        public void TestGetUpdatedFund_IfBelowThresholdLimit_ReturnsSameFund(int fund)
        {
            
            // Act
            double output = wrapper.GetUpdatedFund(fund);

            // Assert
            Assert.True(output == fund);
        }

        [Theory]
        [InlineData(5000, 4980)]
        [InlineData(500, 480)]
        [InlineData(20, 0)]
        [InlineData(78, 58)]
        [InlineData(10, -10)]
        public void TestUpdatedRefundAfterBrokerage_IfBelowLimit_ReturnsRefundMinusMinimumBrokerage(double input, double output)
        {
            
            // Act
            double result = wrapper.UpdatedRefundAfterBrokerage(input);

            // Assert
            Assert.True(result == output);
        }

        [Fact]
        public void TestUpdatedRefundAfterBrokerage_IfAboveLimit_ReturnsRefundMinusBrokerage()
        {
            // Arrange
            double equityPrice = 200000;

            // Act
            double output = wrapper.UpdatedRefundAfterBrokerage(equityPrice);

            // Assert
            Assert.True(output == 199900);
        }
    }

}
