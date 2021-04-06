using System;
using System.Collections.Generic;

namespace SSA.Infrastructure.EF
{
    public partial class Task
    {
        public Task()
        {
            TaskReviewLog = new HashSet<TaskReviewLog>();
        }

        public int IdTask { get; set; }
        public int IdActivity { get; set; }
        public string Title { get; set; }
        public int IdCategory { get; set; }
        public TimeSpan TimeInvested { get; set; }
        public double AdvancedPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Comment { get; set; }
        public int IdUserCreated { get; set; }
        public DateTime RecordDate { get; set; }
        public int? IdUserdUpdated { get; set; }
        public DateTime? RecordUpdate { get; set; }
        public bool RecordStatus { get; set; }
        public int IdStatus { get; set; }

        public Activity IdActivityNavigation { get; set; }
        public Status IdStatusNavigation { get; set; }
        public ICollection<TaskReviewLog> TaskReviewLog { get; set; }
    }
}
