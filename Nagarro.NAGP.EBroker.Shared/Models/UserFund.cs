using System.ComponentModel.DataAnnotations;

namespace Nagarro.NAGP.EBroker.Shared.Models
{
    public class UserFund
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public double Funds { get; set; }
    }
}
