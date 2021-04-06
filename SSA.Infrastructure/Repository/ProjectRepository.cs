using SSA.Core.Coworkers;
using SSA.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSA.Infrastructure.Repository
{
    public class ProjectRepository
    {
        private readonly SSAContext context;

        public ProjectRepository()
        {
            context = new SSAContext();
        }

        public List<int> GetAllCoworkerOfProject(int idProject)
        {
            return context.ProjectCoworker.Where(x => x.IdProject == idProject && x.RecordStatus).Select(y => y.IdProjectCoworker).ToList();
        }

        public List<string> GetExistentProjectName(int exeptId)
        {
            return context.Project.Where(x => x.RecordStatus == true && x.IdProject != exeptId).Select(y => y.Name).ToList();
        }

        public bool SaveChange(SSA.Core.Projects.Project data, int IdUser, ref int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Project tempProject = new Project();
                    bool isUpdate = false;
                    if (data.Id == 0)
                    {
                        tempProject.Name = data.Name;
                        tempProject.Description = data.Description;
                        tempProject.DateSart = data.StartDate;
                        tempProject.DateFinish = data.FinishDate;
                        tempProject.Status = (int)data.Status;
                        tempProject.RecordDate = DateTime.Now;
                        tempProject.RecordStatus = true;
                    }
                    else
                    {
                        isUpdate = true;
                        tempProject = context.Project.Where(x => x.IdProject == data.Id).Select(y => y).FirstOrDefault();
                        tempProject.Name = data.Name;
                        tempProject.Description = data.Description;
                        tempProject.DateSart = data.StartDate;
                        tempProject.DateFinish = data.FinishDate;
                        tempProject.Status = (int)data.Status;
                    }

                    context.Project.Add(tempProject);
                    context.SaveChanges();

                    UpdateTeamMembers(tempProject.IdProject, data.TeamMembers.ToList(), isUpdate, IdUser);
                    context.SaveChanges();
                    transaction.Commit();
                    id = tempProject.IdProject;
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        private void UpdateTeamMembers(int idProject, List<Core.Coworkers.Coworker> teamMembers, bool isUpdate, int IdUser)
        {
            if (!isUpdate)
            {
                foreach (Core.Coworkers.Coworker coworker in teamMembers)
                {
                    var temp = new ProjectCoworker();
                    temp.DateCreated = DateTime.Now;
                    temp.IdCoworker = coworker.Id;
                    temp.IdRolCoworkerProject = coworker.IdRolInProject;
                    temp.IdProject = idProject;
                    temp.IdUserCreated = IdUser;
                    temp.RecordStatus = true;
                    context.ProjectCoworker.Add(temp);

                }
            }
            else
            {
                var listdb = context.ProjectCoworker.Where(x => x.IdProject == idProject).Select(y => y).ToList();
                var news = teamMembers.Where(x => !listdb.Select(s => s.IdCoworker).Contains(x.Id));
                var updated = listdb.Where(x => teamMembers.Select(s => s.Id).Contains(x.IdCoworker)).ToList();
                var deleted = listdb.Where(x => !teamMembers.Select(s => s.Id).Contains(x.IdCoworker));

                foreach (Core.Coworkers.Coworker coworker in news)
                {
                    var temp = new ProjectCoworker();
                    temp.DateCreated = DateTime.Now;
                    temp.IdCoworker = coworker.Id;
                    temp.IdRolCoworkerProject = coworker.IdRolInProject;
                    temp.IdProject = idProject;
                    temp.IdUserCreated = IdUser;
                    temp.RecordStatus = true;
                    context.ProjectCoworker.Add(temp);

                }

                foreach (var item in updated)
                {
                    var temp = teamMembers.Where(x => x.Id == item.IdCoworker).FirstOrDefault();
                    item.IdRolCoworkerProject = temp.IdRolInProject;
                    item.RecordStatus = true;
                    item.RecordUpdated = DateTime.Now;
                    item.IdUserUpdated = IdUser;
                }

                foreach (var item in deleted)
                {
                    item.RecordStatus = false;
                    item.RecordUpdated = DateTime.Now;
                    item.IdUserUpdated = IdUser;
                }
            }
        }

        public SSA.Core.Projects.Project GetInfoProject(int id)
        {
            SSA.Core.Projects.Project _project = new SSA.Core.Projects.Project();
            var info = context.Project.Where(x => x.IdProject == id).Select(y => y).FirstOrDefault();
            if (info == null)
            {
                throw new Exception(string.Format("Project with id : {0} not found into database", id));
            }
            _project.Id = id;
            _project.ChangeName(info.Name, new List<string>());
            _project.Description = info.Description;
            _project.StartDate = info.DateSart;
            _project.FinishDate = info.DateFinish;

            var teamFromDB = context.ProjectCoworker.Where(x => x.IdProject == id).Select(y => new
            {
                Id = y.IdCoworker,
                Rol = y.IdRolCoworkerProject
            }).ToList();

            foreach (var item in teamFromDB)
            {
                Core.Coworkers.Coworker _coworker = new CoworkerRepository().GetCoworkerById(item.Id);
                _coworker.IdRolInProject = item.Rol;
                _project.AddTeamMember(_coworker);
            }
            return _project;
        }

        public List<SSA.Core.Projects.Project> GetInfoAllProject()
        {
            List<SSA.Core.Projects.Project> Projects = new List<Core.Projects.Project>();
            var projects = context.Project.Where(x => x.RecordStatus).Select(y => y).ToList();
            foreach (var info in projects)
            {
                SSA.Core.Projects.Project _project = new SSA.Core.Projects.Project();

                _project.ChangeName(info.Name, new List<string>());
                _project.Description = info.Description;
                _project.StartDate = info.DateSart;
                _project.FinishDate = info.DateFinish;
                _project.Id = info.IdProject;

                var teamFromDB = context.ProjectCoworker.Where(x => x.IdProject == info.IdProject).Select(y => new
                {
                    Id = y.IdCoworker,
                    Rol = y.IdRolCoworkerProject
                }).ToList();

                foreach (var item in teamFromDB)
                {
                    Core.Coworkers.Coworker _coworker = new CoworkerRepository().GetCoworkerById(item.Id);
                    _coworker.IdRolInProject = item.Rol;
                    _project.AddTeamMember(_coworker);
                }

                Projects.Add(_project);
            }

            return Projects;
        }
    }
}
