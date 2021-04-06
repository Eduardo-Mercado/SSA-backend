using System;
using System.Collections.Generic;

namespace SSA.Security.EF
{
    public partial class Option
    {
        public Option()
        {
            RolOption = new HashSet<RolOption>();
        }

        public int IdOption { get; set; }
        public string Name { get; set; }
        public int? IdParent { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public DateTime RecordDate { get; set; }
        public bool RecordStatu { get; set; }
        public int Order { get; set; }

        public ICollection<RolOption> RolOption { get; set; }
    }
}
