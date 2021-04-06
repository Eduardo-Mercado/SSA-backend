using System;
using System.Collections.Generic;

namespace SSA.Infrastructure.EF
{
    public partial class Coworker
    {
        public Coworker()
        {
            ProjectCoworker = new HashSet<ProjectCoworker>();
        }

        public int IdCoworker { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool RecordStatus { get; set; }
        public int IdUserCreated { get; set; }
        public DateTime RecordDate { get; set; }
        public int? IdUserUpdated { get; set; }
        public DateTime? RecordUpdated { get; set; }
        public string ProfilePicture { get; set; }

        public ICollection<ProjectCoworker> ProjectCoworker { get; set; }
    }
}
