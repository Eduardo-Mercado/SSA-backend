using SSA.ApplicationService.DTO.Activity;
using SSA.Infrastructure.Repository;

namespace SSA.ApplicationService.Activities
{
    public class ActivitySummaryService
    {
        public ActivitySummaryDTO GetProjectActivities(int idProject)
        {
            ActivitySummaryDTO info = new ActivitySummaryDTO();
            var projectInfo = new ProjectRepository().GetInfoProject(idProject);

            info.IdProject = idProject;
            info.Project = projectInfo.Name;
            info.StartProject = projectInfo.StartDate;
            info.FinishProject = projectInfo.FinishDate;

            var activitiesByProject = new ActivityRepository().GetAll(idProject);

            foreach (var activity in activitiesByProject)
            {
                ActivityDetailDTO temp = new ActivityDetailDTO();
                temp.IdActivity = activity.IdActivity;
                temp.Priority = ((Core.EnumPriority)activity.IdCategory).ToString();
                temp.Activity = activity.Name;
                temp.AssignedTo = activity.AssignedCoworker;
                temp.Status = activity.Status;
                temp.Activity = activity.Name;
                info.Activities.Add(temp);
            }
            return info;
        }

        public ActivityDTO GetActivityInfo(int idActivity)
        {
            ActivityDTO info = new ActivityDTO();
            var data = new ActivityRepository().GetActivityDisplay(idActivity);
            info.Finish = data.CompletedDate;
            info.Id = idActivity;
            info.Start = data.AssignedDate;
            info.Summary = data.Summary;
            info.Title = data.Name;
            return info;
        }
    }
}
