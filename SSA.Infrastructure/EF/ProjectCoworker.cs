using System;
using System.Collections.Generic;

namespace SSA.Infrastructure.EF
{
    public partial class ProjectCoworker
    {
        public ProjectCoworker()
        {
            Activity = new HashSet<Activity>();
            TaskReviewLog = new HashSet<TaskReviewLog>();
        }

        public int IdProjectCoworker { get; set; }
        public int IdProject { get; set; }
        public int IdCoworker { get; set; }
        public int IdUserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public int? IdUserUpdated { get; set; }
        public DateTime? RecordUpdated { get; set; }
        public bool RecordStatus { get; set; }
        public int IdRolCoworkerProject { get; set; }

        public Coworker IdCoworkerNavigation { get; set; }
        public RolCoworkerProject IdRolCoworkerProjectNavigation { get; set; }
        public ICollection<Activity> Activity { get; set; }
        public ICollection<TaskReviewLog> TaskReviewLog { get; set; }
    }
}
