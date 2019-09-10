using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace MovaClubWebApp.MovaClubDb.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Column("Date")]
        public DateTime BirthdayDate { get; set; }
        public string Gender { get; set; }
        public string ChangeRoleRequest { get; set; }
    }
}
