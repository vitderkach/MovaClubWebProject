using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class Office
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string LocalAdress { get; set; }

        public List<ClassRoom> ClassRooms { get; set; }
        public List<Administrator> Administrators { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
