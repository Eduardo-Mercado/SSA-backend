using SSA.ApplicationService.DTO.Activity;
using SSA.ApplicationService.DTO.Task;
using SSA.Core.Activities;
using SSA.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSA.ApplicationService.Tasks
{
    public class TaskAction : ITaskAction
    {
        private Activity activity;

        public TaskAction(Activity data)
        {
            try
            {
                activity = data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ConfirmChangeTask(int idUser)
        {
            if (this.activity.IdActivity > 0)
            {
                return new ActivityRepository().SaveTasks(this.activity, idUser);
            }
            throw new Exception("Must save Activity before save tasks");
        }

        public TaskDTO GetTaskById(int idTask)
        {
            if (idTask > 0)
            {
                var result = new ActivityRepository().GetTask(idTask);
                var categoryTaskList = new DropDownRepository().CategoryTask();
                return new TaskDTO
                {
                    Category = categoryTaskList.Where(x => x.Id == result.IdCategory).Select(y => y.Value).FirstOrDefault(),
                    Description = result.Description,
                    End = result.DateTimeEnd,
                    Id = result.IdTask,
                    IdCategory = result.IdCategory,
                    ProgressPercent = result.ProgressPercentage,
                    Start = result.DateTimeStart,
                    TimeInvested = result.AmountTime.TotalMilliseconds,
                    Title = result.Title
                };
            }

            throw new Exception("Must provide a valid Id");
        }

        public bool RemoveTask(int id, int idUser)
        {
            this.activity.RemoveTask(id);
            return new ActivityRepository().SaveTasks(this.activity, idUser);
        }

        public bool UpdateTasks(TaskDTO task, bool isInsert, int idUser)
        {
            try
            {
                if (isInsert)
                {
                    Task temp = new Task(id: 0, title: task.Title, descripcion: task.Description, idCategory: task.IdCategory, start: task.Start, end: task.End, amountTime: TimeSpan.FromMilliseconds( task.TimeInvested),
                        progress: task.ProgressPercent);
                    this.activity.AddTask(temp);
                    new ActivityRepository().SaveTasks(this.activity, idUser);
                }
                else
                {
                    Task temp = new Task(id: task.Id, title: task.Title, descripcion: task.Description, idCategory: task.IdCategory, start: task.Start, end: task.End, amountTime: TimeSpan.FromMilliseconds(task.TimeInvested),
                        progress: task.ProgressPercent);
                    this.activity.UpdateTask(temp);
                    new ActivityRepository().SaveTasks(this.activity, idUser);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AuthorizeTasks(List<TaskDTO> tasks, int idUserAuthenticated)
        {
            if (tasks.Count > 0)
            {
                foreach (var item in tasks)
                {
                    var temporalTasks = this.activity.Tasks.Where(x => x.IdTask == item.Id).FirstOrDefault();
                    if (temporalTasks !=  null)
                    {
                        temporalTasks.Status = (item.IsAutorize) ? Core.EnumStatus.Authorized : Core.EnumStatus.Reject;
                       
                    }
                }

                if (tasks.Where(x=>x.IsAutorize).Count() == this.activity.Tasks.Count)
                {
                    this.activity.SetAuthorizeStatus();
                }

                if (new ActivityRepository().SaveTasks(this.activity, idUserAuthenticated))
                {
                    List<SSA.Infrastructure.ReadModel.TaskReviewLog> tasksReview = new List<SSA.Infrastructure.ReadModel.TaskReviewLog>();
                    tasksReview = tasks.Select(y => new SSA.Infrastructure.ReadModel.TaskReviewLog
                    {
                        IdTask = y.Id,
                        Comments = y.ComentAutorize,
                        IsAuthorize = y.IsAutorize
                    }).ToList();

                 return   new ActivityRepository().SaveTaskreviewlog(tasksReview, idUserAuthenticated);
                }
            }

            throw new Exception("No Tasks found");
        }
    }
}
