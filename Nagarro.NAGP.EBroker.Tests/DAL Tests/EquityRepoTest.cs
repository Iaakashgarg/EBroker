using Microsoft.EntityFrameworkCore;
using Moq;
using Nagarro.NAGP.EBroker.DAL.Data;
using Nagarro.NAGP.EBroker.DAL.Repo;
using Nagarro.NAGP.EBroker.Shared.Models;
using System;
using System.Linq;
using Xunit;

namespace Nagarro.NAGP.EBroker.Tests.DAL_Tests
{
    public class EquityRepoTest
    {
        private readonly DbContextOptions<EBrokerDBContext> options;
        
        public EquityRepoTest()
        {
            options = new DbContextOptionsBuilder<EBrokerDBContext>().UseInMemoryDatabase(databaseName: "EquityDatabase").Options;

            using (var context = new EBrokerDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Users.Add(new User()
                {
                    UserId = 1,
                    Name = "Aayush Sharma",
                    Password = "test@123",
                    Funds = 1000
                });
                context.Users.Add(new User()
                {
                    UserId = 2,
                    Name = "Aakash Garg",
                    Password = "test@12345",
                    Funds = 0
                });
                context.Equities.Add(new Equity()
                {
                    EquityId = 1,
                    Name = "Nagarro",
                    Price = 102,
                    AvailableQuantity = 50
                });
                context.Equities.Add(new Equity()
                {
                    EquityId = 2,
                    Name = "ACC",
                    Price = 125,
                    AvailableQuantity = 20
                });
                context.Equities.Add(new Equity()
                {
                    EquityId = 3,
                    Name = "Tata",
                    Price = 20,
                    AvailableQuantity = 10
                });
                context.UserEquities.Add(new UserEquity()
                {
                    UserEquityId = 1,
                    UserId = 1,
                    EquityId = 1,
                    Quantity = 2,
                    Date = DateTime.Now
                });
                context.UserEquities.Add(new UserEquity()
                {
                    UserEquityId = 2,
                    UserId = 1,
                    EquityId = 2,
                    Quantity = 5,
                    Date = DateTime.Now
                });

                context.SaveChanges();
                
            }
        }

        [Fact]
        public void CheckIfEntity_IsFetchedCorrectly_ReturnsTrue()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Act
                var users = context.Users.ToList<User>();

                // Assert
                Assert.True(users.Count == 2);
            }
        }

        [Theory]
        [InlineData(1, 1000, 2000)]
        [InlineData(1, 15, 1015)]
        [InlineData(2, 1890, 1890)]
        public void CheckIfFunds_IsAddedCorrectly_ReturnsTrue(int userId, double input, double output)
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                EquityRepo repo = new EquityRepo(context);

                // Act
                repo.AddFunds(userId, input);
                var user = context.Users.FirstOrDefault(x => x.UserId == userId);

                // Assert
                Assert.True(user.Funds == output);
            }
        }

        [Fact]
        public void CheckIfFunds_IsNotAddedForInvalidUser_ReturnsTrue()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                int userId = 5, Funds = 1000;
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.AddFunds(userId, Funds);

                // Assert
                Assert.Equal("Invalid User", output);
            }
        }

        [Theory]
        [InlineData(1, 425000, 425787.5)]
        [InlineData(1, 560000, 560720)]
        [InlineData(2, 110000, 109945)]
        public void CheckIfFunds_Over1LakhIsAddedWithExtraCharges_ReturnsTrue(int userId, double input, double output)
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                EquityRepo repo = new EquityRepo(context);

                // Act
                var result = repo.AddFunds(userId, input);

                // Assert
                var user = context.Users.FirstOrDefault(x => x.UserId == userId);
                Assert.True(user.Funds == output);
            }
        }

        [Fact]
        public void IfAddingFunds_ForValidValues_ReturnsCorrectMessage()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                int userId = 2, Funds = 13000;
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.AddFunds(userId, Funds);

                // Assert
                Assert.Equal("Funds Added Successfully", output);
            }
        }

        [Fact]
        public void BuyNewEquity_WorkingCorrectly_True()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 2, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.BuyNewEquity(data);

                // Assert
                Assert.Equal("Equity Bought Successfully", output);
            }
        }

        [Fact]
        public void BuyNewEquity_WorkingCorrectlyForUsersWithoutFunds_True()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 2, EquityId = 1, Quantity = 5, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.BuyNewEquity(data);

                // Assert
                Assert.Equal("Insufficient Funds", output);
            }
        }

        [Fact]
        public void BuyNewEquity_GivingErrorMessageForInvalidUserOrEquity_True()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 210, EquityId = 1, Quantity = 5, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.BuyNewEquity(data);

                // Assert
                Assert.Equal("Something went wrong", output);
            }
        }

        [Fact]
        public void BuyNewEquity_CorrectlyUpdatingExistingEquityQuantity_True()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 1, EquityId = 2, Quantity = 2, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.BuyNewEquity(data);

                // Assert
                var userEquity = context.UserEquities.FirstOrDefault(x => x.UserId == data.UserId && x.EquityId == data.EquityId);
                Assert.True(userEquity.Quantity == 7);
            }
        }

        [Fact]
        public void BuyNewEquity_CorrectlyAddNewEquities_True()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 1, EquityId = 3, Quantity = 2, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.BuyNewEquity(data);

                // Assert
                var userEquity = context.UserEquities.FirstOrDefault(x => x.UserId == data.UserId && x.EquityId == data.EquityId);
                Assert.True(userEquity.Quantity == 2);
            }
        }

        [Fact]
        public void BuyNewEquity_CorrectlyUpdatingUserFunds_True()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 1, EquityId = 3, Quantity = 2, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.BuyNewEquity(data);

                // Assert
                var user = context.Users.FirstOrDefault(x => x.UserId == data.UserId);
                Assert.True(user.Funds == 960);
            }
        }

        [Fact]
        public void BuyNewEquity_ForUnavailableRequiredQuantity_WorkingCorrectly()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 1, EquityId = 3, Quantity = 15, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.BuyNewEquity(data);

                // Assert
                Assert.Equal("Something went wrong", output);
            }
        }

        [Fact]
        public void SellEquity_ForValidValues_ReturnsCorrectMessage()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 2, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.SellEquity(data);

                // Assert
                Assert.Equal("Equity Sold Successfully", output);
            }
        }

        [Fact]
        public void SellEquity_ForInvalidUserOrEquityId_ReturnsCorrectMessage()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 200, EquityId = 100, Quantity = 5, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.SellEquity(data);

                // Assert
                Assert.Equal("You can only sell your equities", output);
            }
        }

        [Fact]
        public void SellEquity_ForInvalidQuantity_ReturnsCorrectMessage()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 5, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.SellEquity(data);

                // Assert
                Assert.Equal("You can only sell 2 Equities", output);
            }
        }

        [Fact]
        public void SellEquity_ForSellingAllEquities_WorkingCorrectly()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 1, EquityId = 1, Quantity = 2, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.SellEquity(data);

                // Assert
                var userEquities = context.UserEquities.FirstOrDefault(x => x.UserId == data.UserId && x.EquityId == data.EquityId);
                Assert.Null(userEquities);
            }
        }

        [Fact]
        public void SellEquity_AfterSellingFewEquities_CorrectlyUpdatingQuantity()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 1, EquityId = 2, Quantity = 2, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.SellEquity(data);

                // Assert
                var userEquities = context.UserEquities.FirstOrDefault(x => x.UserId == data.UserId && x.EquityId == data.EquityId);
                Assert.True(userEquities.Quantity == 3);
            }
        }

        [Fact]
        public void SellEquity_AfterSellingEquities_CorrectlyUpdatingFunds()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                UserEquity data = new UserEquity { UserId = 1, EquityId = 2, Quantity = 5, Date = DateTime.Now };
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.SellEquity(data);

                // Assert
                var user = context.Users.FirstOrDefault(x => x.UserId == data.UserId);
                Assert.True(user.Funds == 1605);
            }
        }

        [Fact]
        public void GetUserEquities_ForExistingUserId_ReturnResults()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                int userId = 1;
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.GetUserEquities(userId);

                // Assert
                Assert.NotEmpty(output);
                Assert.NotNull(output);
                Assert.True(output.Count() == 2);
            }
        }

        [Fact]
        public void GetUserEquities_ForInvalidUserId_ReturnEmptyResults()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                int userId = 121;
                EquityRepo repo = new EquityRepo(context);

                // Act
                var output = repo.GetUserEquities(userId);

                // Assert
                Assert.Empty(output);
                Assert.True(output.Count() == 0);
            }
        }

        [Fact]
        public void Is_Dispose_ReleaseMemory_True()
        {
            using (var context = new EBrokerDBContext(options))
            {
                // Arrange
                EquityRepo repo = new EquityRepo(context);

                // Act
                repo.Dispose();

                // Assert
                Assert.True(repo.disposed);
            }
        }
    }
}
