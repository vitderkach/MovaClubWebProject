using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class ExceptLesson
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public bool isEmpty { get; set; }

        public Lesson Lesson { get; set; }

    }
}
