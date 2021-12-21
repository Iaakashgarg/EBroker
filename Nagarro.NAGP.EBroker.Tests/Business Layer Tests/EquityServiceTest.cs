using Moq;
using Nagarro.NAGP.EBroker.Business.Services;
using Nagarro.NAGP.EBroker.DAL.Repo;
using Nagarro.NAGP.EBroker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Nagarro.NAGP.EBroker.Tests.Business_Layer_Tests
{
    public class EquityServiceTest
    {
        private readonly IEquityService equityService;
        private readonly Mock<IEquityRepo> equityRepo;

        public EquityServiceTest()
        {
            equityRepo = new Mock<IEquityRepo>();
            equityService = new EquityService(equityRepo.Object);
        }

        [Fact]
        public void IsAddFunds_LargeFunds_WorkingCorrectly()
        {
            // Arrange
            int userId = 1;
            double Funds = 10000;
            equityRepo.Setup(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>())).Returns("Funds Added Successfully");

            // Act
            string result = equityService.AddFunds(userId, Funds);

            // Assert
            equityRepo.Verify(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>()), Times.Once);
            Assert.Equal("Funds Added Successfully", result);

        }

        [Fact]
        public void IsAddFunds_ForInvalidUser_WorkingCorrectly()
        {
            // Arrange
            int userId = 100;
            double Funds = 1000;
            equityRepo.Setup(x => x.AddFunds(userId, Funds)).Returns("Invalid User");

            // Act
            string result = equityService.AddFunds(userId, Funds);

            // Assert
            equityRepo.Verify(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>()), Times.Once);
            Assert.Equal("Invalid User", result);
        }

        [Fact]
        public void IsAddFunds_ForZeroFunds_WorkingCorrectly()
        {
            // Arrange
            int userId = 1;
            double Funds = 0;

            // Act
            string result = equityService.AddFunds(userId, Funds);

            // Assert
            equityRepo.Verify(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>()), Times.Never);
            Assert.Equal("Funds must be greater than 0", result);
        }

        [Fact]
        public void IsAddFunds_InCaseOfException_ThrowsException()
        {
            // Arrange
            int UserId = 1;
            double Funds = 1200;
            equityRepo.Setup(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>())).Throws(new Exception("Sample Exception"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => equityService.AddFunds(UserId, Funds));
            Assert.Equal("Sample Exception", ex.Message);
        }

        [Fact]
        public void IsBuyNewEquity_ForCorrectValues_WorkingCorrectly()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 2, Date = DateTime.Now };
            equityRepo.Setup(x => x.BuyNewEquity(It.IsAny<UserEquity>())).Returns("Equity Bought Successfully");

            // Act
            string result = equityService.BuyNewEquity(data);

            // Assert
            equityRepo.Verify(x => x.BuyNewEquity(It.IsAny<UserEquity>()), Times.Once);
            Assert.Equal("Equity Bought Successfully", result);

        }

        [Fact]
        public void IsBuyNewEquity_ForZeroQuantity_WorkingCorrectly()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 0, Date = DateTime.Now };
            
            // Act
            string result = equityService.BuyNewEquity(data);

            // Assert
            equityRepo.Verify(x => x.SellEquity(It.IsAny<UserEquity>()), Times.Never);
            Assert.Equal("Quantity must be greater than O", result);
        }

        [Fact]
        public void IsBuyNewEquity_InCaseOfException_ThrowsException()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 1, Date = DateTime.Now };
            equityRepo.Setup(x => x.BuyNewEquity(It.IsAny<UserEquity>())).Throws(new Exception("Manual Exception"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => equityService.BuyNewEquity(data));
            Assert.Equal("Manual Exception", ex.Message);
        }

        [Fact]
        public void IsSellEquity_ForCorrectValues_WorkingCorrectly()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 2, Date = DateTime.Now };
            equityRepo.Setup(x => x.SellEquity(It.IsAny<UserEquity>())).Returns("Equity Sold Successfully");

            // Act
            string result = equityService.SellEquity(data);

            // Assert
            equityRepo.Verify(x => x.SellEquity(It.IsAny<UserEquity>()), Times.Once);
            Assert.Equal("Equity Sold Successfully", result);

        }

        [Fact]
        public void IsSellEquity_ForZeroQuantity_WorkingCorrectly()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 0, Date = DateTime.Now };
            
            // Act
            string result = equityService.SellEquity(data);

            // Assert
            equityRepo.Verify(x => x.SellEquity(It.IsAny<UserEquity>()), Times.Never);
            Assert.Equal("Quantity must be greater than O", result);
        }

        [Fact]
        public void IsSellEquity_InCaseOfException_ThrowsException()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 1, Date = DateTime.Now };
            equityRepo.Setup(x => x.SellEquity(It.IsAny<UserEquity>())).Throws(new Exception("Index out of range"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => equityService.SellEquity(data));
            Assert.Equal("Index out of range", ex.Message);
        }

        [Fact]
        public void IsGetUserEquities_ForValidUserId_ReturningData()
        {
            // Arrange
            int userId = 1;
            var result = new List<UserEquity>
            {
                new UserEquity()
                {
                    UserEquityId = 1,
                    UserId = 1,
                    EquityId = 1,
                    Quantity = 2,
                    Date = DateTime.Now
                },
                new UserEquity()
                {
                    UserEquityId = 2,
                    UserId = 1,
                    EquityId = 2,
                    Quantity = 5,
                    Date = DateTime.Now
                }
            };
            equityRepo.Setup(x => x.GetUserEquities(It.IsAny<int>())).Returns(result);

            //Act
            var output = equityService.GetUserEquities(userId);

            // Assert
            equityRepo.Verify(x => x.GetUserEquities(It.IsAny<int>()), Times.Once);
            Assert.NotNull(output);
            Assert.True(output.Count() == 2);
        }

        [Fact]
        public void IsGetUserEquities_ForInvalidUserId_ReturningEmptyList()
        {
            // Arrange
            int userId = 1000;
            equityRepo.Setup(x => x.GetUserEquities(It.IsAny<int>())).Returns(new List<UserEquity>());

            //Act
            var output = equityService.GetUserEquities(userId);

            // Assert
            Assert.NotNull(output);
            Assert.True(output.Count() == 0);
        }

    }
}
