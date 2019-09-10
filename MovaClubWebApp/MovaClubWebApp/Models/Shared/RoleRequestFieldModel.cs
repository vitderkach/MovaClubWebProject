using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.Models.Shared
{
    public class RoleRequestFieldModel: AbstractSettingsField
    {
        public string RoleRequest { get; set; }
        public string NewRole { get; set; }
    }
}
