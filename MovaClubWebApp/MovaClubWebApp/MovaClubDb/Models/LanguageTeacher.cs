using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class LanguageTeacher
    {
        public int TeacherId { get; set; }
        public int LanguageId { get; set; }
       
        public Teacher Teacher { get; set; }
        public Language Language { get; set; }
    }
}
