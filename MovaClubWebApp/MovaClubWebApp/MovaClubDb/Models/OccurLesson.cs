using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class OccurLesson
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int TeacherId { get; set; }
        [Required]
        [Column("Date")]
        public DateTime LessonDate { get; set; }
        [Required]
        [Column("Time")]
        public DateTime LessonTime { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TeacherCost { get; set; }
        [Required]
        [Column(TypeName ="decimal(18, 2)")]
        public decimal PayedTeacher { get; set; }
        public Teacher Teacher { get; set; }
        public Group Group { get; set; }
    }
}
