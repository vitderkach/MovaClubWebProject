using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.Models.Shared
{
    public class PasswordFieldModel : AbstractSettingsField
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string VisiblePassword { get; set; } 
    }
}
