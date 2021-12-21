using System;

namespace Nagarro.NAGP.EBroker.Shared.Utilities
{
    public class EquityUtility
    {
        private const double fundThreshold = 100000;
        private const double overchargeRate = 0.05;
        private const double brokerageRate = 0.05;
        public const double brokerageCharge = 20;

        public static double GetUpdatedFund(double funds)
        {
            return funds > fundThreshold ? funds - (overchargeRate * funds / 100) : funds;
        }

        public static bool IsValidDate(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && date.TimeOfDay >= new TimeSpan(9, 0, 0) && date.TimeOfDay <= new TimeSpan(15, 0, 0);
        }

        public static double UpdatedRefundAfterBrokerage(double equityPrice)
        {
            double brokerage = (brokerageRate * equityPrice / 100) > brokerageCharge ? (brokerageRate * equityPrice / 100) : brokerageCharge;
            return equityPrice - brokerage;
        }

    }
}
