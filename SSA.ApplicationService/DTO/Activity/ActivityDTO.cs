using System;

namespace SSA.ApplicationService.DTO.Activity
{
    public class ActivityDTO
    {
        public int Id { get; set; }
        public int IdProject { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int CreatedBy { get; set; }
        public int AssignedTo { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public int IdPriority { get; set; } 
    }
}
