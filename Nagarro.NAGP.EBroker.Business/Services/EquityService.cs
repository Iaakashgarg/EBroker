using Nagarro.NAGP.EBroker.DAL.Repo;
using Nagarro.NAGP.EBroker.Shared.Models;
using System;
using System.Collections.Generic;

namespace Nagarro.NAGP.EBroker.Business.Services
{
    public class EquityService : IEquityService
    {
        private readonly IEquityRepo _equityRepo;
        public EquityService(IEquityRepo equityRepo)
        {
            _equityRepo = equityRepo;
        }

        public string AddFunds(int userId, double Funds)
        {
            try
            {
                if (Funds > 0)
                {
                    return _equityRepo.AddFunds(userId, Funds);
                }
                else
                {
                    return "Funds must be greater than 0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserEquity> GetUserEquities(int UserId)
        {
            return _equityRepo.GetUserEquities(UserId);
        }

        public string BuyNewEquity(UserEquity userEquity)
        {
            try
            {
                if (userEquity.Quantity > 0)
                {
                    return _equityRepo.BuyNewEquity(userEquity);
                }
                else
                {
                    return "Quantity must be greater than O";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SellEquity(UserEquity userEquity)
        {
            try
            {
                if (userEquity.Quantity > 0)
                {
                    return _equityRepo.SellEquity(userEquity);
                }
                else
                {
                    return "Quantity must be greater than O";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
