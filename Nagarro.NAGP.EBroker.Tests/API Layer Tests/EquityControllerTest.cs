using Microsoft.AspNetCore.Mvc;
using Moq;
using Nagarro.NAGP.EBroker.API.Controllers;
using Nagarro.NAGP.EBroker.Business.Services;
using Nagarro.NAGP.EBroker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Nagarro.NAGP.EBroker.Tests.API_Layer_Tests
{
    public class EquityControllerTest
    {
        private readonly EquityController equityController;
        private readonly Mock<IEquityService> equityService;

        public EquityControllerTest()
        {
            equityService = new Mock<IEquityService>();
            equityController = new EquityController(equityService.Object);
        }

        [Fact]
        public void BuyNewEquity_Executes_ReturnsOkResponse()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 2, Date = new DateTime(2021, 12, 20, 09, 15, 00) };
            equityService.Setup(x => x.BuyNewEquity(It.IsAny<UserEquity>())).Returns("Equity Bought Successfully");

            // Act
            var result = equityController.BuyNewEquity(data);

            // Assert
            equityService.Verify(x => x.BuyNewEquity(It.IsAny<UserEquity>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            Assert.Equal("Equity Bought Successfully", ((OkObjectResult)result).Value);
        }

        [Fact]
        public void BuyNewEquity_Executes_ReturnsBadResponse()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 2, Date = new DateTime(2021, 12, 20, 19, 15, 00) };

            // Act
            var result = equityController.BuyNewEquity(data);

            // Assert
            equityService.Verify(x => x.BuyNewEquity(It.IsAny<UserEquity>()), Times.Never);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
            Assert.Equal("Invalid Time to Buy Equity", ((BadRequestObjectResult)result).Value);
        }

        [Fact]
        public void BuyNewEquity_Executes_ThrowsException()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 2, Date = new DateTime(2021, 12, 16, 12, 15, 00) };
            equityService.Setup(x => x.BuyNewEquity(It.IsAny<UserEquity>())).Throws(new Exception("DivideByZeroException"));

            // Act
            var result = equityController.BuyNewEquity(data);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public void SellEquity_Executes_ReturnsOkResponse()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 2, Date = new DateTime(2021, 12, 20, 09, 15, 00) };
            equityService.Setup(x => x.SellEquity(It.IsAny<UserEquity>())).Returns("Equity Sold Successfully");

            // Act
            var result = equityController.SellEquity(data);

            // Assert
            equityService.Verify(x => x.SellEquity(It.IsAny<UserEquity>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            Assert.Equal("Equity Sold Successfully", ((OkObjectResult)result).Value);
        }

        [Fact]
        public void SellEquity_Executes_ReturnsBadResponse()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 2, Date = new DateTime(2021, 12, 18, 18, 15, 00) };

            // Act
            var result = equityController.SellEquity(data);

            // Assert
            equityService.Verify(x => x.SellEquity(It.IsAny<UserEquity>()), Times.Never);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
            Assert.Equal("Invalid Time to Sell Equity", ((BadRequestObjectResult)result).Value);
        }

        [Fact]
        public void SellEquity_Executes_ThrowsException()
        {
            // Arrange
            UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 2, Date = new DateTime(2021, 12, 16, 13, 15, 00) };
            equityService.Setup(x => x.SellEquity(It.IsAny<UserEquity>())).Throws(new Exception("DivideByZeroException"));

            // Act
            var result = equityController.SellEquity(data);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public void AddFunds_Executes_ReturnsOkResponse()
        {
            // Arrange
            UserFund data = new UserFund { UserId = 1, Funds = 1000 };
            equityService.Setup(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>())).Returns("Funds Added Successfully");

            // Act
            var result = equityController.AddFunds(data);

            // Assert
            equityService.Verify(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }

        [Fact]
        public void AddFunds_Executes_ReturnsBadRequest()
        {
            // Arrange
            UserFund data = new UserFund { };
            equityService.Setup(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>())).Throws(new Exception("Some Issue Occured"));

            // Act
            var result = equityController.AddFunds(data);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
        }

        [Fact]
        public void AddFunds_ForValidValues_ReturnsCorrectResponse()
        {
            // Arrange
            UserFund data = new UserFund { UserId = 1, Funds = 1000 };
            equityService.Setup(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>())).Returns("Funds Added Successfully");

            // Act
            var result = equityController.AddFunds(data);

            // Assert
            equityService.Verify(x => x.AddFunds(It.IsAny<int>(), It.IsAny<double>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            Assert.IsType<string>(((OkObjectResult)result).Value);
            Assert.Equal("Funds Added Successfully", ((OkObjectResult)result).Value);
        }

        [Fact]
        public void GetUserEquities_ForValidUserId_ReturnsCorrectResponse()
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
            equityService.Setup(x => x.GetUserEquities(It.IsAny<int>())).Returns(result);

            // Act
            var output = equityController.GetUserEquities(userId);

            // Assert
            equityService.Verify(x => x.GetUserEquities(It.IsAny<int>()), Times.Once);
            Assert.NotNull(output);
            Assert.True(output.Count() == 2);
        }

        [Fact]
        public void GetUserEquities_ForInvalidUserId_ReturnsEmptyList()
        {
            // Arrange
            int userId = 1;
            equityService.Setup(x => x.GetUserEquities(It.IsAny<int>())).Returns(new List<UserEquity>());

            // Act
            var output = equityController.GetUserEquities(userId);

            // Assert
            Assert.NotNull(output);
            Assert.True(output.Count() == 0);
        }
    }
}
