using SSA.ApplicationService.DTO.Activity;

namespace SSA.ApplicationService.Activities
{
    public class ActivityService
    {
        private readonly IActivityAction _IActivityAction;

        public ActivityService(IActivityAction activityAction)
        {
            this._IActivityAction = activityAction;
        }
         
        public bool Create(ActivityDTO info, ref int newId)
        {
            return this._IActivityAction.SaveActivity(info, ref newId);
        }

        public bool Update(ActivityDTO info)
        { 
            return this._IActivityAction.UpdateActivity(info);
        }

        public bool Remove(int idUser, ref int newId)
        {
            return this._IActivityAction.RemoveActivity(idUser);
        }
    }
}
