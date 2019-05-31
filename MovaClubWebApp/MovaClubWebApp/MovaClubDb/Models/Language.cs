using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Group> Groups { get; set; }

    }
}
