using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nagarro.NAGP.EBroker.Business.Services;
using Nagarro.NAGP.EBroker.DAL.Data;
using Nagarro.NAGP.EBroker.DAL.Repo;
using Xunit;

namespace Nagarro.NAGP.EBroker.Tests.API_Layer_Tests
{
    public class StartupTest
    {
        [Fact]
        public void Program_Verify_IsHostRunning()
        {
            // Act
            IWebHost webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();

            // Assert
            Assert.NotNull(webHost);
        }

        [Fact]
        public void Startup_Verify_IsServicesNotNull()
        {
            // Act
            IWebHost webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();

            // Assert
            Assert.NotNull(webHost.Services.GetRequiredService<IEquityService>());
            Assert.NotNull(webHost.Services.GetRequiredService<IEquityRepo>());
            Assert.NotNull(webHost.Services.GetRequiredService<EBrokerDBContext>());
        }

        [Fact]
        public void Startup_VerifyService_InstanceCorrect()
        {
            // Act
            IWebHost webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();

            // Assert
            Assert.IsType<EquityService>(webHost.Services.GetRequiredService<IEquityService>());
            Assert.IsType<EquityRepo>(webHost.Services.GetRequiredService<IEquityRepo>());
            Assert.IsType<EBrokerDBContext>(webHost.Services.GetRequiredService<EBrokerDBContext>());
        }
    }

}
