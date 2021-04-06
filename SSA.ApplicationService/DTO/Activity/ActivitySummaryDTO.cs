using System;
using System.Collections.Generic;
using System.Text;

namespace SSA.ApplicationService.DTO.Activity
{
    public class ActivitySummaryDTO
    {
        public int IdProject { get; set; }
        public string Project { get; set; }
        public DateTime StartProject { get; set; }
        public DateTime? FinishProject { get; set; }
        public List<ActivityDetailDTO> Activities { get; set; }

        public ActivitySummaryDTO()
        {
            this.Activities = new List<ActivityDetailDTO>();
        }

    }

    public class ActivityDetailDTO
    {
        public int IdActivity { get; set; }
        public string Activity { get; set; }
        public string Priority { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
    }
}
