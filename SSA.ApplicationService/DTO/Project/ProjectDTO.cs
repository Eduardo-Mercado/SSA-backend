using System;
using System.Collections.Generic;
using System.Text;

namespace SSA.ApplicationService.DTO.Project
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public int Status { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TeamMemberDTO> TeamProject { get; set; }
    }

    public class TeamMemberDTO
    {
        public int IdCoworker { get; set; }
        public int IdRolCoworkerProject { get; set; }
    }


}
