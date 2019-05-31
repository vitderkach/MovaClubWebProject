using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class LanguageLevel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Group> Groups { get; set; }
    }
}
