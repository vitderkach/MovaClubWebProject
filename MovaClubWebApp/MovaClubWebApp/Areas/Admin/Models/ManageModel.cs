using System.Collections.Generic;

namespace MovaClubWebApp.Areas.Admin.Models
{

    public class ViewUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string ChangeRoleRequest { get; set; }
    }


    public class ManageModel
    {
        public List<ViewUser> ViewUsers { get; set; }
    }
}
