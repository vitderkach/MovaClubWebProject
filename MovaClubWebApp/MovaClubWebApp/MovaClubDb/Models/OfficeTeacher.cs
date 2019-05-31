using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class OfficeTeacher
    {
        public int OfficeId { get; set; }
        public int TeacherId { get; set; }

        public Office Office { get; set; }
        public Teacher Teacher { get; set; }
    }
}
