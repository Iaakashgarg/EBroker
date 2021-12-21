using Nagarro.NAGP.EBroker.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nagarro.NAGP.EBroker.Shared.Wrappers
{
    public class Wrapper : IWrapper
    {
        public bool IsValidDate(DateTime date)
        {
            return EquityUtility.IsValidDate(date);
        }

        public double GetUpdatedFund(double funds)
        {
            return EquityUtility.GetUpdatedFund(funds);
        }

        public double UpdatedRefundAfterBrokerage(double equityPrice)
        {
            return EquityUtility.UpdatedRefundAfterBrokerage(equityPrice);
        }
    }
}
