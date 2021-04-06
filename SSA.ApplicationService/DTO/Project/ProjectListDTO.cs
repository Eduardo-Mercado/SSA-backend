using System;
using System.Collections.Generic;
using System.Text;

namespace SSA.ApplicationService.DTO.Project
{
    public class ProjectListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TeamProjectDTO> Team { get; set; }

        public ProjectListDTO()
        {
            this.Team = new List<TeamProjectDTO>();
        }
    }

    public class TeamProjectDTO
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string RolProject { get; set; }
    }
}
