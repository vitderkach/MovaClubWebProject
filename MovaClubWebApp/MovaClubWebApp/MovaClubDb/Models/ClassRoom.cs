using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MovaClubWebApp.MovaClubDb.Models
{
    public class ClassRoom
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OfficeId { get; set; }

        public Office Office { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
    }
}
