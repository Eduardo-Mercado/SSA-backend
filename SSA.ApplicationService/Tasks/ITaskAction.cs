using SSA.ApplicationService.DTO.Task;
using System.Collections.Generic;

namespace SSA.ApplicationService.Tasks
{
    public interface ITaskAction
    {
        bool ConfirmChangeTask(int idUser);
        bool UpdateTasks(DTO.Task.TaskDTO task, bool isInsert, int idUser);
        bool RemoveTask(int id, int idUser);
        DTO.Task.TaskDTO GetTaskById(int idTask);

        bool AuthorizeTasks(List<TaskDTO> tasks, int idUserAuthenticated);
    }
}
