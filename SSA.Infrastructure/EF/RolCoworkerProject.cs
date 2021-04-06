using System;
using System.Collections.Generic;

namespace SSA.Infrastructure.EF
{
    public partial class RolCoworkerProject
    {
        public RolCoworkerProject()
        {
            ProjectCoworker = new HashSet<ProjectCoworker>();
        }

        public int IdRolCoworkerProject { get; set; }
        public string Name { get; set; }
        public bool RecordStatus { get; set; }
        public int IdUserCreated { get; set; }
        public DateTime RecordDate { get; set; }
        public int? IdUserUpdated { get; set; }
        public DateTime? RecordUpdated { get; set; }

        public ICollection<ProjectCoworker> ProjectCoworker { get; set; }
    }
}
