using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nagarro.NAGP.EBroker.DAL.Data;
using System.Linq;
using Xunit;

namespace Nagarro.NAGP.EBroker.Tests.DAL_Tests
{
    public class EBrokerDBContextTest
    {
        private readonly DbContextOptions<EBrokerDBContext> options;

        public EBrokerDBContextTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<EBrokerDBContext>();
            builder.UseInMemoryDatabase(databaseName: "EquityDatabase")
                   .UseInternalServiceProvider(serviceProvider);

            options = builder.Options;

            using (var context = new EBrokerDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            };
        }

        [Fact]
        public void IsSaveData_InitialSeed_WorkingFine()
        {
            // Arrange
            using (var context = new EBrokerDBContext(options))
            {
                // Act
                context.AddTestData();

                // Assert
                Assert.NotEmpty(context.Users);
                Assert.Equal(2, context.Users.Count());
                Assert.Equal(3, context.Equities.Count());
                Assert.Equal(2, context.UserEquities.Count());
            }
        }

    }
}
