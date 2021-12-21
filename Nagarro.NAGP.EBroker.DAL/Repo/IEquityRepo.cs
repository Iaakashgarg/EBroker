using Nagarro.NAGP.EBroker.Shared.Models;
using System;
using System.Collections.Generic;

namespace Nagarro.NAGP.EBroker.DAL.Repo
{
    public interface IEquityRepo : IDisposable
    {
        public string AddFunds(int UserId, double Funds);

        public List<UserEquity> GetUserEquities(int UserId);

        public string BuyNewEquity(UserEquity userEquity);

        public string SellEquity(UserEquity userEquity);

    }
}
