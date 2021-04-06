using System;

namespace SSA.Infrastructure.ReadModel
{
    public class ActivityDisplay
    {
        public int IdActivity { get; set; }
        public int IdProject { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string AssignedCoworker { get; set; }
        public string CreatedBy { get; set; }
        public int IdCategory { get; set; }
        public int IdPriority { get; set; }
        public DateTime AssignedDate { get; set; }
        public float PercentCompleted { get; private set; }
        public string Status { get; set; }
        public DateTime CompletedDate { get; set; }

    }
}
