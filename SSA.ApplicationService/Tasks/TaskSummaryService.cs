using SSA.ApplicationService.DTO.Task;
using SSA.Infrastructure.Repository;
using System;
using System.Linq;

namespace SSA.ApplicationService.Tasks
{
    public class TaskSummaryService
    {
        public TaskSummaryDTO GetSummaryTasks(int idActivity)
        {
            TaskSummaryDTO info = new TaskSummaryDTO();

            var infoActivity = new ActivityRepository().GetActivity(idActivity);

            infoActivity = new ActivityRepository().GetAllTasks(infoActivity);
            var categoryTaskList = new DropDownRepository().CategoryTask();
            info.Activity = infoActivity.Name;
            info.AssignedTo = infoActivity.AssignedCoworker.FullName;
            info.CreatedBy = infoActivity.CreatedBy.FullName;
            info.DescriptionActivity = infoActivity.Summary;
            info.StartActivity = infoActivity.AssignedDate;
            info.EndActivity = infoActivity.CompletedDate;

            if (infoActivity.Tasks != null)
            {
                foreach (var task in infoActivity.Tasks)
                {
                    TaskDTO temp = new TaskDTO();
                    temp.Description = task.Description;
                    temp.End = task.DateTimeEnd;
                    temp.Id = task.IdTask;
                    temp.ProgressPercent = task.ProgressPercentage;
                    temp.Start = task.DateTimeStart;
                    temp.TimeInvested = task.AmountTime.TotalMilliseconds;
                    temp.Title = task.Title;
                    temp.Category = categoryTaskList.Where(x => x.Id == task.IdCategory).Select(y => y.Value).FirstOrDefault();
                    temp.Status = task.Status.ToString();
                    info.Tasks.Add(temp);
                }
                info.TimeInvested = TimeSpan.FromMilliseconds(info.Tasks.Sum(x => x.TimeInvested));
            }

            return info;
        }
    }
}
