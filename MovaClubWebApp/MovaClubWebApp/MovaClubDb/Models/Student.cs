using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MovaClubWebApp.MovaClubDb.Models
{
    public class Student
    {
        [Key]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        public List<Subscription> Subscriptions { get; set; }
    } 
}
