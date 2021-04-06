using System;
using System.Collections.Generic;

namespace SSA.Security.EF
{
    public partial class RolOption
    {
        public int IdRolOption { get; set; }
        public int IdRol { get; set; }
        public int IdOption { get; set; }
        public DateTime RecordDate { get; set; }
        public bool RecordStatus { get; set; }

        public Option IdOptionNavigation { get; set; }
        public Role IdRolNavigation { get; set; }
    }
}
