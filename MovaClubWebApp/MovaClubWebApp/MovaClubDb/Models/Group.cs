using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovaClubWebApp.MovaClubDb.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int LanguageLevelId { get; set; }
        [Required]
        public string Title { get; set; }
        public Language Language { get; set; }
        public LanguageLevel LanguageLevel { get; set; }

        public List<Lesson> Lessons { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
