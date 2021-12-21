using Nagarro.NAGP.EBroker.DAL.Data;
using Nagarro.NAGP.EBroker.Shared.Models;
using Nagarro.NAGP.EBroker.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nagarro.NAGP.EBroker.DAL.Repo
{
    public class EquityRepo : IEquityRepo
    {
        private readonly EBrokerDBContext _eBrokerDBContext;
        public bool disposed = false;

        public EquityRepo(EBrokerDBContext eBrokerDBContext)
        {
            _eBrokerDBContext = eBrokerDBContext;
        }

        public string AddFunds(int UserId, double Funds)
        {
            User user = GetUser(UserId);
            if (user != null)
            {
                Funds = EquityUtility.GetUpdatedFund(Funds);
                user.Funds += Funds;
                _eBrokerDBContext.Users.Update(user);
                _eBrokerDBContext.SaveChanges();
                return "Funds Added Successfully";
            }
            else
            {
                return "Invalid User";
            }
        }

        public List<UserEquity> GetUserEquities(int UserId)
        {
            return _eBrokerDBContext.UserEquities.Where(x => x.UserId == UserId).ToList();
        }

        public string BuyNewEquity(UserEquity userEquity)
        {
            User user = GetUser(userEquity.UserId);
            Equity equity = GetEquity(userEquity.EquityId);
            UserEquity data = GetUserEquity(userEquity.UserId, userEquity.EquityId);
            string message;
            if (equity != null && user != null && equity.AvailableQuantity >= userEquity.Quantity)
            {
                double totalPrice = userEquity.Quantity * equity.Price;
                if (user.Funds >= totalPrice)
                {
                    if (data != null)
                    {
                        data.Quantity += userEquity.Quantity;
                        data.Date = userEquity.Date;
                        _eBrokerDBContext.UserEquities.Update(data);
                    }
                    else
                    {
                        var newEquity = new UserEquity
                        {
                            UserId = userEquity.UserId,
                            EquityId = userEquity.EquityId,
                            Quantity = userEquity.Quantity,
                            Date = userEquity.Date
                        };
                        _eBrokerDBContext.UserEquities.Add(newEquity);
                    }
                    user.Funds -= totalPrice;
                    _eBrokerDBContext.Users.Update(user);
                    equity.AvailableQuantity -= userEquity.Quantity;
                    _eBrokerDBContext.Equities.Update(equity);
                    _eBrokerDBContext.SaveChanges();
                    message = "Equity Bought Successfully";
                }
                else
                {
                    message = "Insufficient Funds";
                }
            }
            else
            {
                message = "Something went wrong";
            }
            return message;

        }

        public string SellEquity(UserEquity userEquity)
        {
            User user = GetUser(userEquity.UserId);
            Equity equity = GetEquity(userEquity.EquityId);
            UserEquity data = GetUserEquity(userEquity.UserId, userEquity.EquityId);
            if (user != null && equity != null && data != null)
            {
                double totalPrice = userEquity.Quantity * equity.Price;
                if (userEquity.Quantity > data.Quantity)
                {
                    return "You can only sell " + data.Quantity + " Equities";
                }
                else if (data.Quantity == userEquity.Quantity)
                {
                    _eBrokerDBContext.UserEquities.Remove(data);
                }
                else
                {
                    data.Quantity -= userEquity.Quantity;
                    data.Date = userEquity.Date;
                    _eBrokerDBContext.UserEquities.Update(data);
                }
                equity.AvailableQuantity += userEquity.Quantity;
                _eBrokerDBContext.Equities.Update(equity);
                totalPrice = EquityUtility.UpdatedRefundAfterBrokerage(totalPrice);
                user.Funds += totalPrice;
                _eBrokerDBContext.Users.Update(user);
                _eBrokerDBContext.SaveChanges();
                return "Equity Sold Successfully";
            }
            else
            {
                return "You can only sell your equities";
            }

        }

        private User GetUser(int id)
        {
            return _eBrokerDBContext.Users.Where(a => a.UserId == id).SingleOrDefault();
        }
        private Equity GetEquity(int id)
        {
            return _eBrokerDBContext.Equities.Where(a => a.EquityId == id).SingleOrDefault();
        }

        private UserEquity GetUserEquity(int userId, int equityId)
        {
            return _eBrokerDBContext.UserEquities.Where(a => a.UserId == userId && a.EquityId == equityId).SingleOrDefault();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _eBrokerDBContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
