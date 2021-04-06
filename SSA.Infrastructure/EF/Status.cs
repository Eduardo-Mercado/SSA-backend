using System;
using System.Collections.Generic;

namespace SSA.Infrastructure.EF
{
    public partial class Status
    {
        public Status()
        {
            Activity = new HashSet<Activity>();
            Task = new HashSet<Task>();
        }

        public int IdStatus { get; set; }
        public string Name { get; set; }
        public bool RecordStatus { get; set; }
        public string Color { get; set; }

        public ICollection<Activity> Activity { get; set; }
        public ICollection<Task> Task { get; set; }
    }
}
