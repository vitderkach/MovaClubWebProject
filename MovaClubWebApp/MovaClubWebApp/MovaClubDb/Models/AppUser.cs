using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace MovaClubWebApp.MovaClubDb.Models
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Column("Date")]
        public DateTime BirthdayDate { get; set; }

        //public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }

    //public class AppRole: IdentityRole
    //{
    //    public virtual ICollection<AppUserRole> UserRoles { get; set; }
    //}

    //public class AppUserRole : IdentityUserRole<string>
    //{
    //    public virtual AppUser User { get; set; }
    //    public virtual AppRole Role { get; set; }
    //}

}
