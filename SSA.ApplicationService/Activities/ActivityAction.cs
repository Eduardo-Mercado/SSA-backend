using SSA.ApplicationService.DTO.Activity;
using SSA.Core.Activities;
using SSA.Infrastructure.Repository;
using System;
using System.Collections.Generic;

namespace SSA.ApplicationService.Activities
{
    public class ActivityAction : IActivityAction
    {
        private Activity activity;

        public ActivityAction()
        {
           
        }

        public bool RemoveActivity(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveActivity(ActivityDTO data, ref int id)
        {
            Core.Coworkers.Coworker coworkerAssigned = new CoworkerRepository().GetCoworkerByIdProjectCoworker(data.AssignedTo);
            //Core.Coworkers.Coworker coworkerCreatorActivity = new CoworkerRepository().GetCoworkerById(data.CreatedBy);
            Core.Coworkers.Coworker coworkerCreatorActivity = new CoworkerRepository().GetCoworkerByIdProjectCoworker(data.AssignedTo);
            List<int> idCoworkersForProject = new ProjectRepository().GetAllCoworkerOfProject(data.IdProject);

            activity = new Activity();
            activity.AssignedDate = data.Start;
            activity.CompletedDate = data.Finish;
            activity.IdActivity = 0;
            activity.IdCategory = data.IdPriority;
            activity.IdProject = data.IdProject;
            activity.Name = data.Title;
            activity.Summary = data.Summary;
            activity.SetAssignCoworker(coworkerAssigned, idCoworkersForProject);
            activity.SetCreatorActivity(coworkerCreatorActivity, idCoworkersForProject);
             
            if (new ActivityRepository().Save(activity, 0, ref id))
            {
                return true;
            }
            return false;
        }

        public bool UpdateActivity(ActivityDTO data)
        {
            Core.Coworkers.Coworker coworkerAssigned = new CoworkerRepository().GetCoworkerById(data.AssignedTo);
            Core.Coworkers.Coworker coworkerCreatorActivity = new CoworkerRepository().GetCoworkerById(data.CreatedBy);
            List<int> idCoworkersForProject = new ProjectRepository().GetAllCoworkerOfProject(data.IdProject);

            activity = new Activity();
            activity.AssignedDate = data.Start;
            activity.CompletedDate = data.Finish;
            activity.IdActivity = data.Id;
            activity.IdCategory = data.IdPriority;
            activity.IdProject = data.IdProject;
            activity.Name = data.Title;
            activity.Summary = data.Summary;
            activity.SetAssignCoworker(coworkerAssigned, idCoworkersForProject);
            activity.SetCreatorActivity(coworkerCreatorActivity, idCoworkersForProject);

            return new ActivityRepository().Update(activity);
        }

    }
}
