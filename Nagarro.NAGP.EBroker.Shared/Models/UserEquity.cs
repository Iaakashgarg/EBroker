using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nagarro.NAGP.EBroker.Shared.Models
{
    public class UserEquity
    {
        public int UserEquityId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int EquityId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
