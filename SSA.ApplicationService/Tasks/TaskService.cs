using SSA.ApplicationService.DTO.Task;
using System;
using System.Collections.Generic;

namespace SSA.ApplicationService.Tasks
{
    public class TaskService
    {
        private readonly ITaskAction _ITaskAction;

        public TaskService(ITaskAction taskAction)
        {
            this._ITaskAction = taskAction;
        }

        public bool UpdateTask(TaskDTO task, bool isInsert, int idUser)
        {
            return this._ITaskAction.UpdateTasks(task, isInsert, idUser);
        }

        public bool ConfirmTask(int idUser, ref int newId)
        {
            return this._ITaskAction.ConfirmChangeTask(idUser);
        }

        public TaskDTO GetInfoTask(int id)
        {
            return this._ITaskAction.GetTaskById(id);
        }

        public bool AuthorizeTasks(List<TaskDTO> tasks, int idUserAuthenticated)
        {
            return this._ITaskAction.AuthorizeTasks(tasks, idUserAuthenticated);
        }

        public bool RemoveTask(int idTask, int IdUser)
        {
            return this._ITaskAction.RemoveTask(idTask, IdUser);
        }
    }
}
