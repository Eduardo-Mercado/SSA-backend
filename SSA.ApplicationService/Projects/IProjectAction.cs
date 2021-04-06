using SSA.ApplicationService.DTO.Project;
using SSA.Core.Coworkers;
using System.Collections.Generic;

namespace SSA.ApplicationService.Projects
{
    public interface IProjectAction
    {
        bool Add(ProjectDTO data);
        bool UpdateStatus(int id, int status);
        bool UpdateTeamMembers(List<Coworker> ids, bool isInsert);
        bool RemoveTeamMember(int id);
        bool ConfirmChange(ref int id);
        ProjectListDTO GetBriefInfoProject(int id);
        List<ProjectListDTO> GetAllProjects();
    }
}
