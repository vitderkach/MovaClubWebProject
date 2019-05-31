using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class Teacher
    {
        [Key]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal IndividualRate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GroupRate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}
