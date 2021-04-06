using System;
using System.Collections.Generic;

namespace SSA.Infrastructure.EF
{
    public partial class TaskReviewLog
    {
        public int IdTaskReviewLog { get; set; }
        public int IdTask { get; set; }
        public string Comment { get; set; }
        public int IdCoworkerReview { get; set; }
        public DateTime RecordDate { get; set; }
        public bool RecordStatus { get; set; }
        public bool Approved { get; set; }

        public ProjectCoworker IdCoworkerReviewNavigation { get; set; }
        public Task IdTaskNavigation { get; set; }
    }
}
