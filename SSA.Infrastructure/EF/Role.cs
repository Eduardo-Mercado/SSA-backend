using System;
using System.Collections.Generic;

namespace SSA.Infrastructure.EF
{
    public partial class Role
    {
        public Role()
        {
            RolOption = new HashSet<RolOption>();
        }

        public int IdRole { get; set; }
        public string Name { get; set; }
        public DateTime RecordDate { get; set; }
        public bool RecordStatu { get; set; }

        public ICollection<RolOption> RolOption { get; set; }
    }
}
