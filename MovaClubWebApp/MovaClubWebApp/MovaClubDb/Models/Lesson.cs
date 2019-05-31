using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public int ClassRoomId { get; set; }
        public int? GroupId { get; set; }
        public int? TeacherId { get; set; }
        [Column("Date")]
        public DateTime StartDate { get; set; }
        [Column("Time")]
        public DateTime Duration { get; set; }
        [Column("Date")]
        public DateTime EndDate { get; set; }

        public Office Office { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public Group Group { get; set; }
        public Teacher Teacher { get; set; }
    }
}
