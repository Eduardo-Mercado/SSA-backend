using SSA.Infrastructure.EF;
using SSA.Infrastructure.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using ActivityEF = SSA.Infrastructure.EF.Activity;
using TaskEF = SSA.Infrastructure.EF.Task;

namespace SSA.Infrastructure.Repository
{
    public class ActivityRepository
    {
        private readonly SSAContext context;

        public ActivityRepository()
        {
            context = new SSAContext();
        }

        public Core.Activities.Activity GetActivity(int IdActivity)
        {
            Core.Activities.Activity temp = new Core.Activities.Activity();
            var data = context.Activity.Find(IdActivity);
            temp.AssignedDate = data.StartDate;
            temp.CompletedDate = data.DueDate;
            temp.IdActivity = IdActivity;
            temp.IdCategory = data.IdCategory;
            temp.IdProject = data.IdProject;
            temp.Name = data.Title;
            temp.Summary = data.Description;
            List<int> idsCoworkerInProject = new ProjectRepository().GetAllCoworkerOfProject(data.IdProject);

            Core.Coworkers.Coworker coworkerAssigned = new CoworkerRepository().GetCoworkerByIdProjectCoworker(data.IdAssignedTo);
            temp.SetAssignCoworker(coworkerAssigned, idsCoworkerInProject);

            Core.Coworkers.Coworker coworkerCreated = new CoworkerRepository().GetCoworkerByIdProjectCoworker(data.IdCreatedBy);
            temp.SetCreatorActivity(coworkerCreated, idsCoworkerInProject);

            return temp;
        }

        public ActivityDisplay GetActivityDisplay(int IdActivity)
        {
            ActivityDisplay temp = new ActivityDisplay();
            var data = context.Activity.Find(IdActivity);
            temp.AssignedDate = data.StartDate;
            temp.CompletedDate = data.DueDate;
            temp.IdActivity = IdActivity;
            temp.IdCategory = data.IdCategory;
            temp.IdProject = data.IdProject;
            temp.Name = data.Title;
            temp.Summary = data.Description;

            Core.Coworkers.Coworker coworkerAssigned = new CoworkerRepository().GetCoworkerByIdProjectCoworker(data.IdAssignedTo);
            temp.AssignedCoworker = coworkerAssigned.FullName;

            Core.Coworkers.Coworker coworkerCreated = new CoworkerRepository().GetCoworkerByIdProjectCoworker(data.IdCreatedBy);
            temp.CreatedBy = coworkerCreated.FullName;

            var enumDisplayStatus = (Core.EnumStatus)data.IdStatus;
            temp.Status = enumDisplayStatus.ToString();
            return temp;
        }

        public Core.Activities.Activity GetAllTasks(Core.Activities.Activity activity)
        {
            var listTasks = context.Task.Where(x => x.IdActivity == activity.IdActivity && x.RecordStatus).ToList();

            foreach (var item in listTasks)
            {
                Core.Activities.Task temporal = new Core.Activities.Task(
                     item.IdTask
                     , item.Title
                     , item.Comment
                     , item.IdCategory
                     , item.StartDate
                     , item.FinishDate
                     , item.TimeInvested
                     , (float)item.AdvancedPercent
                     , (Core.EnumStatus)item.IdStatus
                     );
                //activity.Tasks.Add(temporal);
                activity.AddTask(temporal);
            }
            return activity;
        }

        public List<ActivityDisplay> GetAll(int idProject)
        {
            try
            {
                List<ActivityDisplay> activityList = new List<ActivityDisplay>();

                var info = context.Activity
                    .Where(x => x.IdProject == idProject && x.RecordStatus)
                    .Select(y => y).ToList();

                foreach (var data in info)
                {
                    ActivityDisplay temp = new ActivityDisplay();
                    temp.IdCategory = data.IdCategory;
                    temp.IdProject = data.IdProject;
                    temp.Name = data.Title;
                    temp.IdActivity = data.IdActitivity;
                    Core.Coworkers.Coworker coworkerAssigned = new CoworkerRepository().GetCoworkerByIdProjectCoworker(data.IdAssignedTo);
                    temp.AssignedCoworker = coworkerAssigned.FullName;
                    var enumDisplayStatus = (Core.EnumStatus)data.IdStatus;
                    temp.Status = enumDisplayStatus.ToString();
                    activityList.Add(temp);
                }

                return activityList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveTaskreviewlog(List<SSA.Infrastructure.ReadModel.TaskReviewLog> tasks, int idUserAuthenticated)
        {
            int idCoworker = context.Account.Find(idUserAuthenticated).IdCoworker;
            int idProject = context.Task.Join(context.Activity, t => t.IdActivity, a => a.IdActitivity, (t, a) => new { t = t, a = a })
                                    .Where(x => x.t.IdTask == tasks[0].IdTask)
                                    .Select(y => y.a.IdProject).FirstOrDefault();
            int idCoworkerProject = context.ProjectCoworker.Where(x => x.IdProject == idProject && x.IdCoworker == idCoworker).Select(y => y.IdProjectCoworker).FirstOrDefault();
            foreach (var item in tasks)
            {
                EF.TaskReviewLog temp = new EF.TaskReviewLog();
                temp.Approved = item.IsAuthorize;
                temp.Comment = item.Comments;
                temp.IdCoworkerReview = idCoworkerProject;
                temp.IdTask = item.IdTask;
                temp.RecordDate = DateTime.Now;
                temp.RecordStatus = true;

                context.TaskReviewLog.Add(temp);
            }

            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Save(Core.Activities.Activity record, int idUser, ref int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    ActivityEF temp = new ActivityEF();
 
                    if (record.IdActivity == 0)
                    {
                        temp.Title = record.Name;
                        temp.Description = record.Summary;
                        temp.DueDate = record.CompletedDate;
                        temp.IdAssignedTo = record.AssignedCoworker.Id;
                        temp.IdCategory = record.IdCategory;
                        temp.IdCreatedBy = record.CreatedBy.Id;
                        temp.IdPriority = record.IdPriority;
                        temp.IdProject = record.IdProject;
                        temp.IdStatus = GetInitialState();
                        temp.StartDate = record.AssignedDate;
                        temp.IdUserCreated = idUser;
                        temp.RecordDate = DateTime.Now;
                        temp.RecordStatus = true;
                        context.Activity.Add(temp);
                    }
                    else
                    {
                        temp = context.Activity.Where(x => x.IdActitivity == record.IdActivity).FirstOrDefault();
                        temp.Description = record.Summary;
                        temp.DueDate = record.CompletedDate;
                        temp.IdAssignedTo = record.AssignedCoworker.Id;
                        temp.IdCategory = record.IdCategory;
                        temp.IdCreatedBy = record.CreatedBy.Id;
                        temp.IdPriority = record.IdPriority;
                        temp.IdStatus = GetInitialState();
                        temp.IdUserUpdated = idUser;
                        temp.RecordUpdated = DateTime.Now;
                        temp.RecordStatus = true;
                    }

                    context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool Update(Core.Activities.Activity activity)
        {
            try
            {
                var activityFromDB = context.Activity.Find(activity.IdActivity);

                if (activityFromDB == null)
                {
                    throw new Exception("Activity not found");
                }

                activityFromDB.Description = activity.Summary;
                activityFromDB.Title = activity.Name;
                activityFromDB.DueDate = activity.CompletedDate;
                activityFromDB.IdAssignedTo = activity.AssignedCoworker.Id;
                activityFromDB.IdCategory = activity.IdCategory;
                activityFromDB.IdCreatedBy = activity.CreatedBy.Id;
                activityFromDB.IdUserCreated = 1;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool SaveTasks(Core.Activities.Activity activity, int idUser)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var activityFromDB = context.Activity.Find(activity.IdActivity);
                    activityFromDB.IdStatus = (int)activity.Status; // (int)Core.EnumStatus.InPoggress;
                    activityFromDB.RecordUpdated = DateTime.Now;
                    activityFromDB.IdUserUpdated = idUser;

                    var taskFromDB = context.Task.Where(x => x.IdActivity == activity.IdActivity).Select(y => y).ToList();
                    var newTasks = activity.Tasks.Where(x => x.IdTask == 0).Select(y => y).ToList();


                    foreach (var item in newTasks)
                    {
                        var task = new TaskEF();
                        task.IdActivity = activity.IdActivity;
                        task.IdCategory = item.IdCategory;
                        task.IdUserCreated = idUser;
                        task.StartDate = item.DateTimeStart;
                        task.FinishDate = item.DateTimeEnd;
                        task.AdvancedPercent = item.ProgressPercentage;
                        task.Comment = item.Description;
                        task.TimeInvested = item.AmountTime;
                        task.RecordDate = DateTime.Now;
                        task.RecordStatus = true;
                        task.Title = item.Title;
                        task.IdStatus = GetInitialStateForTask();
                        context.Task.Add(task);

                    }


                    if (taskFromDB.Count > 0)
                    {
                        var updated = taskFromDB.Where(x => activity.Tasks.Where(s => s.IdTask > 0).Select(y => y.IdTask).Contains(x.IdTask)).ToList();
                        var deleted = taskFromDB.Where(x => !activity.Tasks.Where(s => s.IdTask > 0).Select(y => y.IdTask).Contains(x.IdTask)).ToList();
                        foreach (var item in updated)
                        {
                            var temp = activity.Tasks.Where(x => x.IdTask == item.IdTask).FirstOrDefault();
                            item.IdCategory = temp.IdCategory;
                            item.AdvancedPercent = temp.ProgressPercentage;
                            item.Comment = temp.Description;
                            item.FinishDate = temp.DateTimeEnd;
                            item.RecordStatus = true;
                            item.TimeInvested = temp.AmountTime;
                            item.Title = temp.Title;
                            item.IdUserdUpdated = idUser;
                            item.RecordUpdate = DateTime.Now;
                            context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        foreach (var item in deleted)
                        {
                            item.RecordStatus = false;
                            item.IdUserdUpdated = idUser;
                            item.RecordUpdate = DateTime.Now;
                            context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }
                    context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public Core.Activities.Task GetTask(int id)
        {
            var info = context.Task.Find(id);
            return new Core.Activities.Task(info.IdTask, info.Title, info.Comment, info.IdCategory, info.StartDate, info.FinishDate, info.TimeInvested, (float)info.AdvancedPercent, (Core.EnumStatus)info.IdStatus);
        }

        private int GetInitialState()
        {
            return context.Status.Where(x => x.Name.ToLower() == "initial").Select(y => y.IdStatus).FirstOrDefault();
        }
        private int GetInitialStateForTask()
        {
            return context.Status.Where(x => x.Name.ToLower() == "Under Revision").Select(y => y.IdStatus).FirstOrDefault();
        }
    }
}
