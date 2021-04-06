using System;
using System.Collections.Generic;
using System.Text;

namespace SSA.ApplicationService.DTO.Task
{
   public class TaskSummaryDTO
    {
        public string Activity { get; set; }
        public DateTime StartActivity { get; set; }
        public DateTime EndActivity { get; set; }
        public string DescriptionActivity { get; set; }
        public string CreatedBy { get; set; }
        public string AssignedTo { get; set; }
        public List<TaskDTO> Tasks { get; set; }
        public TimeSpan TimeInvested { get; set; }
        public TaskSummaryDTO()
        {
            this.Tasks = new List<TaskDTO>();
        }
    }
}
