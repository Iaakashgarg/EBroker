using System;

namespace Nagarro.NAGP.EBroker.Shared.Wrappers
{
    public interface IWrapper
    {
        public bool IsValidDate(DateTime date);

        public double GetUpdatedFund(double funds);

        public double UpdatedRefundAfterBrokerage(double equityPrice);
    }
}
