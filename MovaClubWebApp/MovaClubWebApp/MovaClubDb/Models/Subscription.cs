using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int? GroupId { get; set; } 
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cost { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Payed { get; set; }
        public DateTime EndValidityPeriod { get; set; }
        [Required]
        public DateTime Duration { get; set; }
        [Required]
        public bool Activated { get; set; }

        public Group Group { get; set; }
        public Student Student { get; set; }
        public List<Payment> Payments { get; set; }

    }
}
