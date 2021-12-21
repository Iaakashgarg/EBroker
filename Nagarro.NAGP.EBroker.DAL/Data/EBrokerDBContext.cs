using Microsoft.EntityFrameworkCore;
using Nagarro.NAGP.EBroker.Shared.Models;
using System;

namespace Nagarro.NAGP.EBroker.DAL.Data
{
    public class EBrokerDBContext : DbContext
    {
        public EBrokerDBContext(DbContextOptions<EBrokerDBContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Equity> Equities { get; set; }

        public DbSet<UserEquity> UserEquities { get; set; }

        public void AddTestData()
        {

            var testUser1 = new User
            {
                UserId = 1001,
                Name = "Luke",
                Password = "Skywalker",
                Funds = 0
            };

            Users.Add(testUser1);

            var testUser2 = new User
            {
                UserId = 1002,
                Name = "Aakash",
                Password = "password@123",
                Funds = 1020
            };

            Users.Add(testUser2);


            var testEquity1 = new Equity
            {
                EquityId = 9001,
                Name = "Nokia",
                Price = 98.03,
                AvailableQuantity = 5
            };

            Equities.Add(testEquity1);

            var testEquity2 = new Equity
            {
                EquityId = 9002,
                Name = "Jio",
                Price = 7500.00,
                AvailableQuantity = 30
            };

            Equities.Add(testEquity2);

            var testEquity3 = new Equity
            {
                EquityId = 9003,
                Name = "JSW",
                Price = 18.00,
                AvailableQuantity = 100
            };

            Equities.Add(testEquity3);

            var testUserEquity1 = new UserEquity
            {
                UserEquityId = 1,
                UserId = 1001,
                EquityId = 9001,
                Quantity = 3,
                Date = DateTime.Now
            };

            UserEquities.Add(testUserEquity1);

            var testUserEquity2 = new UserEquity
            {
                UserEquityId = 2,
                UserId = 1002,
                EquityId = 9002,
                Quantity = 2,
                Date = DateTime.Now
            };

            UserEquities.Add(testUserEquity2);
            SaveChanges();

        }
    }
}
