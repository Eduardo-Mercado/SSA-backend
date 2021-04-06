using SSA.ApplicationService.DTO.Activity;

namespace SSA.ApplicationService.Activities
{
    public  interface IActivityAction
    { 
        bool SaveActivity(ActivityDTO activity, ref int id);
        bool UpdateActivity(ActivityDTO activity);
        bool RemoveActivity(int id); 
    }
}
