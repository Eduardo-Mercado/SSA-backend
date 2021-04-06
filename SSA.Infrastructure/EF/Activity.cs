using System;
using System.Collections.Generic;

namespace SSA.Infrastructure.EF
{
    public partial class Activity
    {
        public Activity()
        {
            Task = new HashSet<Task>();
        }

        public int IdActitivity { get; set; }
        public int IdAssignedTo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdStatus { get; set; }
        public int IdCategory { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public int IdUserCreated { get; set; }
        public DateTime RecordDate { get; set; }
        public int? IdUserUpdated { get; set; }
        public DateTime? RecordUpdated { get; set; }
        public int IdCreatedBy { get; set; }
        public int IdPriority { get; set; }
        public bool RecordStatus { get; set; }
        public int IdProject { get; set; }

        public ProjectCoworker IdAssignedToNavigation { get; set; }
        public Project IdProjectNavigation { get; set; }
        public Status IdStatusNavigation { get; set; }
        public ICollection<Task> Task { get; set; }
    }
}
