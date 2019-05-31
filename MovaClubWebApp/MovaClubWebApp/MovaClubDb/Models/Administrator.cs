using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MovaClubWebApp.MovaClubDb.Models
{
    public class Administrator
    {
        [Key]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int OfficeId { get; set; }
        public Office Office { get; set; }
    }
}
