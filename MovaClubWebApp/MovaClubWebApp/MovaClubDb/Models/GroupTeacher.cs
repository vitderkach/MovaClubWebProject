using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class GroupTeacher
    {
        public int GroupId { get; set; }
        public int TeacherId { get; set; }

        public Group Group { get; set; }
        public Teacher Teacher { get; set; }
    }
}
