using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.Models.Shared
{
    public abstract class AbstractSettingsField
    {
        public string UserName { get; set; }
        public bool ContentOpened { get; set; }
        public string ConfirmText { get; set; }
    }
}
