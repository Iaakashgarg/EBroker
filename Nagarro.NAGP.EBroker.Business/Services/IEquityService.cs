using Nagarro.NAGP.EBroker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nagarro.NAGP.EBroker.Business.Services
{
    public interface IEquityService
    {
        public string AddFunds(int userId, double Funds);

        public List<UserEquity> GetUserEquities(int UserId);

        public string BuyNewEquity(UserEquity userEquity);

        public string SellEquity(UserEquity userEquity);
    }
}
