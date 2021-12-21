using System.ComponentModel.DataAnnotations;

namespace Nagarro.NAGP.EBroker.Shared.Models
{
    public class Equity
    {
        public int EquityId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int AvailableQuantity { get; set; }
    }
}
