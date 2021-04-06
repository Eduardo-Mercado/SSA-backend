using SSA.ApplicationService.DTO.Project;
using SSA.Core.Coworkers;
using SSA.Core.Projects;
using SSA.Core.Seed;
using SSA.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSA.ApplicationService.Projects
{
    public class ProjectAction : IProjectAction
    {
        private Project project;

        public bool Add(ProjectDTO data)
        {
            try
            {
                List<string> NamesProject = new ProjectRepository().GetExistentProjectName(0);

                project = new Project();

                project.ChangeName(data.Title, NamesProject);
                project.StartDate = data.StartDate;
                project.FinishDate = data.EndDate;
                project.Description = data.Description;

                foreach (TeamMemberDTO member in data.TeamProject)
                {
                    Coworker _coworker = new CoworkerRepository().GetCoworkerById(member.IdCoworker);
                    _coworker.IdRolInProject = member.IdRolCoworkerProject;
                    this.project.AddTeamMember(_coworker);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ConfirmChange(ref int id)
        {
            int IdRolSenior = 1;// get from database.
            int CountSeniors = this.project.TeamMembers.Where(x => x.IdRolInProject == IdRolSenior).Count();
            if (CountSeniors > 0)
            {
                int idUser = 1; //take for session.
                return new ProjectRepository().SaveChange(this.project, idUser, ref id);

            }
            else
            {
                throw new ArgumentException("Error, at least, one senior must be setup in this project");

            }
        }

        public bool RemoveTeamMember(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(int id, int status)
        {
            project.ChangeStatus(Core.EnumStatus.Active);
            return true;
        }

        public bool UpdateTeamMembers(List<Coworker> coworkers, bool isInsert)
        {
            foreach (Coworker member in coworkers)
            {
                this.project.AddTeamMember(member);
            }

            return true;
        }

        public List<ProjectListDTO> GetAllProjects()
        {
            List<DropDown> listRol = new DropDownRepository().RolCoworkerProject();
            List<ProjectListDTO> finalListProjects = new List<ProjectListDTO>();
            var projects = new ProjectRepository().GetInfoAllProject();
            foreach (var info in projects)
            {
                ProjectListDTO temp = new ProjectListDTO();
                var infoProject = new ProjectRepository().GetInfoProject(info.Id);
                temp.Id = infoProject.Id;
                temp.Description = infoProject.Description;
                temp.Title = infoProject.Name;
                temp.Team = infoProject.TeamMembers.Select(y => new TeamProjectDTO
                {
                    Name = y.FullName,
                    Avatar = y.ProfilePicture,
                    RolProject = listRol.Where(x => x.Id == y.IdRolInProject).Select(s => s.Value).FirstOrDefault()
                }).ToList();

                finalListProjects.Add(temp);
            }

            return finalListProjects;
        }

        public ProjectListDTO GetBriefInfoProject(int id)
        {
            List<DropDown> listRol = new DropDownRepository().RolCoworkerProject();
            ProjectListDTO temp = new ProjectListDTO();
            var infoProject = new ProjectRepository().GetInfoProject(id);
            temp.Description = infoProject.Description;
            temp.Title = infoProject.Name;
            temp.Team = infoProject.TeamMembers.Select(y => new TeamProjectDTO
            {
                Name = y.FullName,
                Avatar = y.ProfilePicture,
                RolProject = listRol.Where(x => x.Id == y.IdRolInProject).Select(s => s.Value).FirstOrDefault()
            }).ToList();

            return temp;
        }
    }
}
