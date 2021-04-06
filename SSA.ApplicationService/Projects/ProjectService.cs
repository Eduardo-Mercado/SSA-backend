using SSA.ApplicationService.DTO.Project;
using System.Collections.Generic;

namespace SSA.ApplicationService.Projects
{
    public class ProjectService
    {
        private readonly IProjectAction _IProjectAction;

        public ProjectService(IProjectAction projectAction)
        {
            this._IProjectAction = projectAction;
        }

        public bool CreateProject(ref ProjectDTO data)
        {
            if (this._IProjectAction.Add(data))
            {
                int idTemp = 0;
                bool ValueReturn = this._IProjectAction.ConfirmChange(ref idTemp);
                data.Id = idTemp;
                return ValueReturn;
            }
            return false;
        }

        public ProjectListDTO GetProject(int id)
        {
            return this._IProjectAction.GetBriefInfoProject(id);
        }

        public List<ProjectListDTO> GetAllProjects()
        {
            return this._IProjectAction.GetAllProjects();
        }
    }
}
