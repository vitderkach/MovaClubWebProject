using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace MovaClubWebApp.MovaClubDb.Models
{
    public enum ExchangeSystem
    {
        Portmone,
        CashPayment
    }

    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int SubscriptionId { get; set; }
        [Required]
        public ExchangeSystem ExchangeSystem { get; set; }
        [Required]
        public string ExchangeId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cost { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Refund { get; set; }

        public Subscription Subscription { get; set; }
    }
}
