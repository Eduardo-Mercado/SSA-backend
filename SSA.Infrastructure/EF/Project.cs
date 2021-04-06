using System;
using System.Collections.Generic;

namespace SSA.Infrastructure.EF
{
    public partial class Project
    {
        public Project()
        {
            Activity = new HashSet<Activity>();
        }

        public int IdProject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int IduserCreated { get; set; }
        public int? IdUserUpdated { get; set; }
        public DateTime RecordDate { get; set; }
        public bool RecordStatus { get; set; }
        public DateTime DateSart { get; set; }
        public DateTime? DateFinish { get; set; }
        public DateTime? RecordUpdated { get; set; }

        public ICollection<Activity> Activity { get; set; }
    }
}
